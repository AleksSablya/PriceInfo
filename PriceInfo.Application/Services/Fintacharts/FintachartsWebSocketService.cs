using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PriceInfo.Domain.Entities;
using PriceInfo.Domain.Fintacharts;
using PriceInfo.Domain.Interfaces;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;

namespace PriceInfo.Application.Services.Fintacharts
{
    public class FintachartsWebSocketService : IFintachartsWebSocketService
    {
        private const string _path = "/api/streaming/ws/v1/realtime?token=";
        private readonly FintachartsApiOptions _options;
        private readonly IFintachartsApiService _fintachartsApiService;
        private readonly ILogger<FintachartsWebSocketService> _logger;

        public FintachartsWebSocketService(
            IFintachartsApiService fintachartsApiService,
            IOptions<FintachartsApiOptions> options,
            ILogger<FintachartsWebSocketService> logger)
        {
            _options = options.Value;
            _fintachartsApiService = fintachartsApiService;
            _logger = logger;
        }

        public async Task<IEnumerable<UpdateInfo>> GetUpdates(string instrumentId, int fetchCount = 1)
        {
            var updateResult = new List<UpdateInfo>();
            using var webSocket = new ClientWebSocket();
            try
            {
                var cancelationToken = CancellationToken.None;
                var accessToken = await _fintachartsApiService.GetAccessTokenAsync(cancelationToken);

                await webSocket.ConnectAsync(await GetUrl(), cancelationToken);
                if (webSocket.State != WebSocketState.Open)
                {
                    _logger.LogError("Cannot connect to socket");
                    return updateResult;
                }
                var buffer = new Memory<byte>(new byte[4096]);

                ValueWebSocketReceiveResult result = await webSocket.ReceiveAsync(buffer, cancelationToken);
                var json = Encoding.UTF8.GetString(buffer.ToArray() ?? Array.Empty<byte>(), 0, result.Count);
                var sessionResponse = JsonSerializer.Deserialize<SessionResponse>(json);
                if (sessionResponse?.Type != "session" || string.IsNullOrEmpty(sessionResponse?.SessionId))
                {
                    _logger.LogError("Cannot get session Id");
                    return updateResult;
                }

                var request = new Request
                {
                    Type = "l1-subscription",
                    Id = "1",
                    // AUD/SGD
                    InstrumentId = instrumentId,
                    Kinds = new[] { "last" }
                };
                var bytes = new ReadOnlyMemory<byte>(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(request)));
                await webSocket.SendAsync(bytes, System.Net.WebSockets.WebSocketMessageType.Text, true, cancelationToken);

                result = await webSocket.ReceiveAsync(buffer, cancelationToken);
                json = Encoding.UTF8.GetString(buffer.ToArray() ?? Array.Empty<byte>(), 0, result.Count);

                sessionResponse = JsonSerializer.Deserialize<SessionResponse>(json);
                if (sessionResponse?.Type != "response" || string.IsNullOrEmpty(sessionResponse?.RequestId) || !string.IsNullOrEmpty(sessionResponse?.Error?.Code))
                {
                    _logger.LogError("Cannot get response: {ErrorCode} {ErrorMessage}", sessionResponse?.Error?.Code, sessionResponse?.Error?.Message);
                    return updateResult;
                }
                int count = 0;
                while (webSocket.State == WebSocketState.Open && count < fetchCount)
                {
                    try
                    {
                        result = await webSocket.ReceiveAsync(buffer, cancelationToken);
                        json = Encoding.UTF8.GetString(buffer.ToArray() ?? Array.Empty<byte>(), 0, result.Count);
                        var updateData = JsonSerializer.Deserialize<UpdateResponse>(json) ?? new UpdateResponse();
                        var kind = updateData?.Last;
                        if (kind != null)
                        {
                            count++;
                            updateResult.Add(new UpdateInfo
                            {
                                InstrumentId = instrumentId,
                                UpdateTime = kind.Timestamp,
                                Price = kind.Price
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        await Task.Delay(200);
                        _logger.LogError(ex, "Error {error} with State: {state}", ex.Message, ((WebSocketState)webSocket.State).ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

            }
            return updateResult;
        }
        private async Task<Uri> GetUrl()
        {
            string accessToken = await _fintachartsApiService.GetAccessTokenAsync(default);
            if (string.IsNullOrEmpty(accessToken))
            {
                _logger.LogCritical("Cannot get access token");
                accessToken = string.Empty;
            }
            return new Uri(_options.UriWss + _path + accessToken);
        }
    }
}

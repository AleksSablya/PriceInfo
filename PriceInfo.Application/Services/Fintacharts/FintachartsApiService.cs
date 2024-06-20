using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PriceInfo.Domain.Fintacharts;
using PriceInfo.Domain.Interfaces;
using System.Text.Json;

namespace PriceInfo.Application.Services.Fintacharts
{
    public class FintachartsApiService : IFintachartsApiService
    {
        const string _AccessTokenUrl = "identity/realms/fintatech/protocol/openid-connect/token";
        const string _InstrumentsUrl = "api/instruments/v1/instruments?provider=simulation&kind=forex";

        private static string _accessToken = string.Empty;
        private static DateTime _expiredAt = default;
        private readonly FintachartsApiOptions _options;
        private readonly ILogger<FintachartsApiService> _logger;

        public FintachartsApiService(IOptions<FintachartsApiOptions> options,
            ILogger<FintachartsApiService> logger)
        {
            _options = options.Value;
            _logger = logger;
        }

        public async Task<string> GetAccessTokenAsync(CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(_accessToken) || _expiredAt < DateTime.Now)
            {
                var httpClient = GetHttpClient();
                try
                {
                    using var httpResponse = await httpClient.PostAsync(_AccessTokenUrl, GetRequest(), cancellationToken);

                    if (httpResponse != null && httpResponse.IsSuccessStatusCode)
                    {
                        var json = await httpResponse.Content.ReadAsStringAsync();
                        var tokenResponse = JsonSerializer.Deserialize<TokenResponse>(json);
                        _accessToken = tokenResponse?.AccessToken ?? string.Empty;
                        _expiredAt = DateTime.Now.AddSeconds(tokenResponse?.ExpiresIn ?? 0);
                    }
                    else
                    {
                        _logger.LogError("Get token error. Server returns {Code}, message {}",
                            httpResponse?.StatusCode ?? System.Net.HttpStatusCode.InternalServerError,
                            httpResponse?.ReasonPhrase ?? string.Empty);
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, ex.Message);
                    throw;
                }
            }
            return _accessToken;
        }

        public async Task<InstrumentsResponse> GetInstrumentsAsync(CancellationToken cancellationToken)
        {
            var token = await GetAccessTokenAsync(cancellationToken);
            HttpClient httpClient = GetHttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("bearer", token);

            using var httpResponse = await httpClient.GetAsync(_InstrumentsUrl, cancellationToken);
            if (httpResponse != null && httpResponse.IsSuccessStatusCode)
            {
                var json = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonSerializer.Deserialize<InstrumentsResponse>(json);
                return response ?? new InstrumentsResponse();
            }
            _logger.LogError("Server returns {Code}, message {}",
                httpResponse?.StatusCode ?? System.Net.HttpStatusCode.InternalServerError,
                httpResponse?.ReasonPhrase ?? string.Empty);
            return new InstrumentsResponse();
        }

        private HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_options.Uri);
            return httpClient;
        }

        private FormUrlEncodedContent GetRequest()
        {
            var request = new FormUrlEncodedContent(new[]
            {
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("client_id", "app-cli"),
                    new KeyValuePair<string, string>("username", _options.UserName),
                    new KeyValuePair<string, string>("password", _options.Password)
                });
            return request;
        }
    }
}

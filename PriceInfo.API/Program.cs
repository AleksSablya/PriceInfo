using Microsoft.EntityFrameworkCore;
using PriceInfo.Application.Services.Fintacharts;
using PriceInfo.Domain.Interfaces;
using PriceInfo.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("AppDb") ??
        throw new InvalidOperationException("Connection string 'AppDb' not found.")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOptions<FintachartsApiOptions>()
    .Configure<IConfiguration>((options, config) => config.GetSection(FintachartsApiOptions.Section).Bind(options));

builder.Services.AddSingleton<IFintachartsApiService, FintachartsApiService>();
builder.Services.AddHostedService<FintachartsWorker>();

builder.Services.AddScoped<IFintachartsWebSocketService,  FintachartsWebSocketService>();
builder.Services.AddScoped<IFintachartsLogic, FintachartsLogic>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Hello World!");

app.Run();

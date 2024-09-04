using Microsoft.EntityFrameworkCore;
using RoboTrader.Domain.Repositories;
using RobotTrader.Infrastructure;
using RobotTrader.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options => 
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    }
);
var externalUrl = builder.Configuration.GetSection("ExternalApiSettings:CoinCapApiUrl");
builder.Services.Configure<ExternalApiSettings>(builder.Configuration.GetSection("ExternalApiSettings:CoinCapApiUrl"));
builder.Services.AddHttpClient<ICryptoMarketDataProvider,CoinCapMarketDataProvider>();


builder.Services.AddScoped<ITradeRepository,TradeRepository>();


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

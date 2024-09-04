using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RoboTrader.Domain.Repositories;
using RobotTrader.Application;
using RobotTrader.Application.DTOs;
using System.Text.Json;

namespace RobotTrader.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketDataController : ControllerBase
    {
        private readonly ICryptoMarketDataProvider _cryptoMarketDataProvider;

        public MarketDataController(ICryptoMarketDataProvider cryptoMarketDataProvider)
        {
            _cryptoMarketDataProvider = cryptoMarketDataProvider;
        }

        [HttpGet("assets")]
        public async Task<IActionResult> GetAssets()
        {

            var assetsStrings = await _cryptoMarketDataProvider.GetAllAssetsAsync();

            var assetsDto = JsonSerializer.Deserialize<AssetsRootDto> (assetsStrings);

            return Ok(assetsDto);
        }

        [HttpGet("assets/{name}")]
        public async Task<IActionResult> GetAssets(string name)
        {
            var assetString = await _cryptoMarketDataProvider.GetMarketDataByNameAsync(name);

            var assetDto = JsonSerializer.Deserialize<AssetRootDto>(assetString);

            return Ok(assetDto);
        }

        /// <summary>
        /// Para saber el valor de conversion de una moneda fiat o cripto
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("rates/{name}")]
        public async Task<IActionResult> GetRate(string name)
        {
            var assetString = await _cryptoMarketDataProvider.GetMarketDataByNameAsync(name);

            var assetDto = JsonSerializer.Deserialize<AssetRootDto>(assetString);

            return Ok(assetDto);
        }
    }
}

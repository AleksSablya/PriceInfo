using Microsoft.AspNetCore.Mvc;
using PriceInfo.Domain.Entities;
using PriceInfo.Domain.Interfaces;
using System.Net;

namespace PriceInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IFintachartsLogic _fintachartsLogic;

        public PriceController(IFintachartsLogic fintachartsLogic)
        {
            _fintachartsLogic = fintachartsLogic;
        }

        // GET api/<PriceController>/AUD/CAD,GBP/CHF
        [HttpGet("{asset}")]
        public async Task<ActionResult<IEnumerable<AssetPriceInfo>>> Get(string? asset)
        {
            var decoded = WebUtility.UrlDecode(asset);
            var result = await _fintachartsLogic.GetPriceInfo(decoded);
            return Ok(result);
        }
    }
}

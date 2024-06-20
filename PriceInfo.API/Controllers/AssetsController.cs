using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PriceInfo.Domain.Entities;
using PriceInfo.Infrastructure.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PriceInfo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssetsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AssetsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AssetDto>>> GetAssets()
        {
            return Ok(await _context.Assets.ToListAsync());
        }
    }
}

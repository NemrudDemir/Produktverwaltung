using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Produktverwaltung.Database;
using Produktverwaltung.Database.Models;
using Produktverwaltung.Extensions;
using Produktverwaltung.Pagination;
using System.Linq;
using System.Threading.Tasks;

namespace Produktverwaltung.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ProductContext context, ILogger<ProductsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(IQueryable<Product>))]
        public IActionResult Get([FromQuery]PaginationParameter paginationParameter)
        {
            var products = _context.Products.Pagination(paginationParameter);
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 200, Type = typeof(Product))]
        [ProducesResponseType(statusCode: 404)]
        public async Task<IActionResult> GetOne(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(statusCode: 200, Type = typeof(Product))]
        [ProducesResponseType(statusCode: 400)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<IActionResult> Post([FromBody]Product product)
        {
            if (await _context.Products.FindAsync(product.Id) != null)
                return Conflict();

            var entry = await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok(entry.Entity);
        }

        [HttpPatch]
        [ProducesResponseType(statusCode: 200, Type = typeof(Product))]
        [ProducesResponseType(statusCode: 400)]
        [ProducesResponseType(statusCode: 404)]
        public async Task<IActionResult> Patch([FromBody]Product product)
        {
            if (await _context.Products.FindAsync(product.Id) == null)
                return NotFound();

            var entry = _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return Ok(entry.Entity);
        }

        [HttpDelete]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 404)]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

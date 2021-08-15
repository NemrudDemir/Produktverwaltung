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
    public class CategoriesController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(ProductContext context, ILogger<CategoriesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(IQueryable<Category>))]
        public IActionResult Get([FromQuery] PaginationParameter paginationParameter)
        {
            var categories = _context.Categories.Pagination(paginationParameter);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 200, Type = typeof(Category))]
        [ProducesResponseType(statusCode: 404)]
        public async Task<IActionResult> GetOne(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpPost]
        [ProducesResponseType(statusCode: 200, Type = typeof(Category))]
        [ProducesResponseType(statusCode: 400)]
        [ProducesResponseType(statusCode: 409)]
        public async Task<IActionResult> Post([FromBody] Category category)
        {
            if (await _context.Categories.FindAsync(category.Id) != null)
                return Conflict();

            var entry = await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return Ok(entry.Entity);
        }

        [HttpPatch]
        [ProducesResponseType(statusCode: 200, Type = typeof(Category))]
        [ProducesResponseType(statusCode: 400)]
        [ProducesResponseType(statusCode: 404)]
        public async Task<IActionResult> Patch([FromBody] Category category)
        {
            if (await _context.Categories.FindAsync(category.Id) == null)
                return NotFound();

            var entry = _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return Ok(entry.Entity);
        }

        [HttpDelete]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 404)]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

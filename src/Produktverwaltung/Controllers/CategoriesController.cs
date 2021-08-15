using Microsoft.AspNetCore.Mvc;
using Produktverwaltung.DataAccess.Entities;
using Produktverwaltung.Repository.Interfaces;
using Produktverwaltung.Repository.Pagination;
using System.Linq;
using System.Threading.Tasks;

namespace Produktverwaltung.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _repo;

        public CategoriesController(ICategoryRepository repository)
        {
            _repo = repository;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(IQueryable<Category>))]
        public IActionResult Get([FromQuery] PaginationParameter paginationParameter)
        {
            var categories = _repo.GetCategories(paginationParameter);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 200, Type = typeof(Category))]
        [ProducesResponseType(statusCode: 404)]
        public async Task<IActionResult> GetOne(int id)
        {
            var category = await _repo.GetCategory(id);
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
            if (await _repo.GetCategory(category.Id) != null)
                return Conflict();

            var entity = await _repo.PostCategory(category);
            
            return Ok(entity);
        }

        [HttpPatch]
        [ProducesResponseType(statusCode: 200, Type = typeof(Category))]
        [ProducesResponseType(statusCode: 400)]
        [ProducesResponseType(statusCode: 404)]
        public async Task<IActionResult> Patch([FromBody] Category category)
        {
            if (await _repo.GetCategory(category.Id) == null)
                return NotFound();

            var entity = await _repo.PatchCategory(category);
            
            return Ok(entity);
        }

        [HttpDelete]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 404)]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _repo.GetCategory(id);
            if (category == null)
                return NotFound();

            await _repo.DeleteCategory(category);
            
            return NoContent();
        }
    }
}

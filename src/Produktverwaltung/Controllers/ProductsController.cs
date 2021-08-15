using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Produktverwaltung.DataAccess.Entities;
using Produktverwaltung.Repository.Interfaces;
using Produktverwaltung.Repository.Pagination;
using System.Linq;
using System.Threading.Tasks;

namespace Produktverwaltung.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductRepository repository, ILogger<ProductsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(statusCode: 200, Type = typeof(IQueryable<Product>))]
        public IActionResult Get([FromQuery]PaginationParameter paginationParameter)
        {
            var products = _repository.GetProducts(paginationParameter);
            return Ok(products);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(statusCode: 200, Type = typeof(Product))]
        [ProducesResponseType(statusCode: 404)]
        public async Task<IActionResult> GetOne(int id)
        {
            var product = await _repository.GetProduct(id);
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
            if (await _repository.GetProduct(product.Id) != null)
                return Conflict();

            var entity = await _repository.PostProduct(product);
            return Ok(entity);
        }

        [HttpPatch]
        [ProducesResponseType(statusCode: 200, Type = typeof(Product))]
        [ProducesResponseType(statusCode: 400)]
        [ProducesResponseType(statusCode: 404)]
        public async Task<IActionResult> Patch([FromBody]Product product)
        {
            if (await _repository.GetProduct(product.Id) == null)
                return NotFound();

            var entity = await _repository.PatchProduct(product);
            return Ok(entity);
        }

        [HttpDelete]
        [ProducesResponseType(statusCode: 204)]
        [ProducesResponseType(statusCode: 404)]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _repository.GetProduct(id);
            if (product == null)
                return NotFound();

            await _repository.DeleteProduct(product);
            return NoContent();
        }
    }
}

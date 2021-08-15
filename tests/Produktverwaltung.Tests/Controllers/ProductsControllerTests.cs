using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using Produktverwaltung.Controllers;
using Produktverwaltung.DataAccess.Entities;
using Produktverwaltung.Repository.Interfaces;
using Produktverwaltung.Repository.Pagination;
using System.Linq;
using System.Threading.Tasks;

namespace Produktverwaltung.Tests.Controllers
{
    public class ProductsControllerTests
    {
        [Test]
        public void GetShouldReturnOkActionResult()
        {
            var returningQueryable = new[] {
                new Product { Id = 1 },
                new Product { Id = 2 }
            }.AsQueryable();
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.GetProducts(It.IsAny<PaginationParameter>())).Returns(returningQueryable);
            var classUnderTest = new ProductsController(repoMock.Object);

            classUnderTest.Get(null).Should().BeOfType<OkObjectResult>().Which.Value.Should().BeSameAs(returningQueryable);
        }

        [Test]
        public async Task GetOneWithNoProductFoundShouldReturnNotFoundResult()
        {
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.GetProduct(It.IsAny<int>())).ReturnsAsync(default(Product));
            var classUnderTest = new ProductsController(repoMock.Object);

            var result = await classUnderTest.GetOne(0);
            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task GetOneWithProductFoundShouldReturnOkObjectResult()
        {
            var returningProduct = new Product { Id = 1, CategoryId = 1, Description = "TestDesc", Name = "TestName", Price = 500 };
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.GetProduct(It.IsAny<int>())).ReturnsAsync(returningProduct);
            var classUnderTest = new ProductsController(repoMock.Object);

            var result = await classUnderTest.GetOne(0);
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeSameAs(returningProduct);
        }

        [Test]
        public async Task PostWithProductFoundShouldReturnConflictResult()
        {
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.GetProduct(It.IsAny<int>())).ReturnsAsync(new Product());
            var classUnderTest = new ProductsController(repoMock.Object);

            var result = await classUnderTest.Post(new Product());
            result.Should().BeOfType<ConflictResult>();
        }

        [Test]
        public async Task PostWithNoProductFoundShouldReturnOkObjectResult()
        {
            var productToPost = new Product() { Id = 123, Price = 456 };
            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.PostProduct(It.IsAny<Product>())).ReturnsAsync(productToPost);
            var classUnderTest = new ProductsController(repoMock.Object);

            var result = await classUnderTest.Post(productToPost);
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeSameAs(productToPost);
        }

        [Test]
        public async Task PatchWithProductFoundShouldReturnOkObjectResult()
        {
            var productToPatch = new Product() { Id = 123, Price = 456 };

            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.GetProduct(It.IsAny<int>())).ReturnsAsync(new Product());
            repoMock.Setup(r => r.PatchProduct(It.IsAny<Product>())).ReturnsAsync(productToPatch);
            var classUnderTest = new ProductsController(repoMock.Object);

            var result = await classUnderTest.Patch(new Product());
            result.Should().BeOfType<OkObjectResult>().Which.Value.Should().BeSameAs(productToPatch);
        }

        [Test]
        public async Task PatchWithNoProductFoundShouldReturnNotFoundResult()
        {
            var repoMock = new Mock<IProductRepository>();
            var classUnderTest = new ProductsController(repoMock.Object);

            var result = await classUnderTest.Patch(new Product());
            result.Should().BeOfType<NotFoundResult>();
        }

        [Test]
        public async Task DeleteWithProductFoundShouldReturnNoContent()
        {
            var productToPatch = new Product() { Id = 123, Price = 456 };

            var repoMock = new Mock<IProductRepository>();
            repoMock.Setup(r => r.GetProduct(It.IsAny<int>())).ReturnsAsync(new Product());
            var classUnderTest = new ProductsController(repoMock.Object);

            var result = await classUnderTest.Delete(0);
            result.Should().BeOfType<NoContentResult>();
        }

        [Test]
        public async Task DeleteWithNoProductFoundShouldReturnNotFoundResult()
        {
            var repoMock = new Mock<IProductRepository>();
            var classUnderTest = new ProductsController(repoMock.Object);

            var result = await classUnderTest.Delete(0);
            result.Should().BeOfType<NotFoundResult>();
        }
    }
}
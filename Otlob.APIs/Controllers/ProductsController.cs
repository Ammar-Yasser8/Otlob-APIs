using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otlob.Core.IRepositories;
using Otlob.Core.Models;

namespace Otlob.APIs.Controllers
{
   
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        public ProductsController(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        // GET: api/Products
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync();
            return Ok(products);    
        }
        // GET: api/Products/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _productRepository.GetAsync(id);
            if (product == null)
            {
                return NotFound(new {Message="Not found",StatusCode=404});
            }
            return Ok(product);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otlob.APIs.DTOs;
using Otlob.Core.IRepositories;
using Otlob.Core.Models;

namespace Otlob.APIs.Controllers
{
   
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IMapper _mapper;   
        public ProductsController(IGenericRepository<Product> productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }
        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReverseDto>>> GetProducts()
        {
            var products = await _productRepository.GetAllAsync(Includes: "ProductBrand,ProductCategory");
            return Ok(_mapper.Map<IEnumerable<Product> , IEnumerable<ProductToReverseDto>>(products));    
        }
        // GET: api/Products/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReverseDto>> GetProduct(int id)
        {
            var product = await _productRepository.GetAsync(product => product.Id == id , Includes:"ProductBrand,ProductCategory");
            if (product == null)
            {
                return NotFound(new { Message = "Not found", StatusCode = 404 });
            }
            return Ok(_mapper.Map<Product,ProductToReverseDto>(product));

        }
    }
}

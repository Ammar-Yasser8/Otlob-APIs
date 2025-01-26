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
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductCategory> _productCategoryRepository;
        private readonly IMapper _mapper;   
        public ProductsController(IGenericRepository<Product> productRepository, IMapper mapper,
            IGenericRepository<ProductBrand> productBrandRepository, 
            IGenericRepository<ProductCategory> productCategoryRepository)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _productBrandRepository = productBrandRepository;
            _productCategoryRepository = productCategoryRepository;
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
        // Get : api/Products/brands
        [HttpGet("brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrands()
        {
            var brands = await _productBrandRepository.GetAllAsync();
            return Ok(brands);
        }
        // Get : api/Products/brands/id
        [HttpGet("brands/{id}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrand(int id)
        {
            var brand = await _productBrandRepository.GetAsync(brand => brand.Id == id);
            if (brand == null)
            {
                return NotFound(new { Message = "Not found", StatusCode = 404 });
            }
            return Ok(brand);
        }

        // Get : api/Products/categories
        [HttpGet("categories")]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            var categories = await _productCategoryRepository.GetAllAsync();
            return Ok(categories);
        }
        // Get : api/Products/categories/id
        [HttpGet("categories/{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int id)
        {
            var category = await _productCategoryRepository.GetAsync(category => category.Id == id);
            if (category == null)
            {
                return NotFound(new { Message = "Not found", StatusCode = 404 });
            }
            return Ok(category);
        }


    }
}

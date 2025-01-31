using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otlob.Core.IRepositories;
using Otlob.Core.Models;

namespace Otlob.APIs.Controllers
{
    public class CategoryController : BaseApiController
    {
        private readonly IGenericRepository<ProductCategory> _categoryRepository;
        public CategoryController(IGenericRepository<ProductCategory> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        // GET: api/Category
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategory>>> GetProductCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return Ok(categories);
        }
        // GET: api/Category/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategory>> GetProductCategory(int id)
        {
            var category = await _categoryRepository.GetAsync(id);
            if (category == null)
            {
                return NotFound(new { Message = "Not found", StatusCode = 404 });
            }
            return Ok(category);
        }



    }
}

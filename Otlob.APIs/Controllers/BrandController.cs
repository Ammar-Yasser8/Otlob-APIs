using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Otlob.Core.IRepositories;
using Otlob.Core.Models;

namespace Otlob.APIs.Controllers
{
    public class BrandController : BaseApiController
    {
        private readonly IGenericRepository<ProductBrand> _brandRepository;
        public BrandController(IGenericRepository<ProductBrand> brandRepository)
        {
            _brandRepository = brandRepository;
        }
        // Get : api/Brand
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrands()
        {
            var brands = await _brandRepository.GetAllAsync();
            return Ok(brands);
        }
        // Get : api/Brand/id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductBrand>> GetProductBrand(int id)
        {
            var brand = await _brandRepository.GetAsync(id);
            if (brand == null)
            {
                return NotFound(new { Message = "Not found", StatusCode = 404 });
            }
            return Ok(brand);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using ECommerce_API.Core.Contracts;
using ECommerce_API.Core.Models;
using ECommerce_API.Core.Models.Category;

using ECommerce_API.Core.Models.Product;
using ECommerce_API.Core.Services;


namespace ECommerce_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesServices _services;

        public CategoriesController(CategoriesServices services)
        {
            this._services = services;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<PagedResult<BaseCategoryDto>>> GetCategories([FromQuery] QueryParameters queryParameters)
        {
            var categories = await _services.getAsyncAllCategories(queryParameters);

            if (categories == null)
            {
                return NotFound();
            }

            return Ok(categories);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BaseCategoryDto>> GetCategoryById(int id)
        {
            var category = await _services.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);

        }
    }

}

using ECommerce_API.Core.Models;
using ECommerce_API.Core.Models.Product;
using ECommerce_API.Core.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace ECommerce_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductCategoryController : ControllerBase
    {
        private readonly ProductCategoryServices _services;
        public ProductCategoryController(ProductCategoryServices services)
        {
            _services = services;
        }


        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<List<BaseProductDto>>> SearchProducts([FromQuery] string searchParameter)
        {
            var searchString = searchParameter.Trim();

            var products = await _services.SearchProducts(searchString);
            // check size of products
            if ( products.Count() == 0 )
            {
                return NotFound();
            }

            return Ok(products);
        }
    }
}

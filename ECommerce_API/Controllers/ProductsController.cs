using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.OData.Query;
using AutoMapper;
using ECommerce_API.Core.Contracts;
using ECommerce_API.Core.Models;
using ECommerce_API.Core.Models.Product;
using ECommerce_API.Core.Services;


namespace ECommerce_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsServices _services;

        public ProductsController( ProductCategoryServices services)
        {
            this._services = _services;
        }

        [HttpGet]
        [EnableQuery]
        public async Task<ActionResult<PagedResult<BaseProductDto>>> GetProducts([FromQuery] QueryParameters queryParameters)
        {
            var products = await _services.getAsyncAllProducts(queryParameters);
            if (products == null)
            {
                return NotFound();
            }
            return Ok(products);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<BaseProductDto>> GetCountryById(int id)
        {
            var product = await _services.GetProductById(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);

        }
    }

}

using ECommerce_API.Core.Contracts;
using ECommerce_API.Core.Models.Product;
using ECommerce_API.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce_API.Core.Services
{
    public class ProductsServices
    {

        private readonly IProductsRepository _productsRepository;

        public ProductsServices( IProductsRepository productsRepository)
        {
            this._productsRepository = productsRepository;
        }

        public async Task<PagedResult<BaseProductDto>> getAsyncAllProducts(QueryParameters queryParameters)
        {
            var products = await _productsRepository.GetAllAsync<BaseProductDto>(queryParameters);

            return products;
        }

        public async Task<BaseProductDto> GetProductById(int id)
        {
            var product = await _productsRepository.GetAsync<BaseProductDto>(id);

            return product;
        }


    }
}

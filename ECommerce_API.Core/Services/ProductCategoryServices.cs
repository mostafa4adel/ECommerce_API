using ECommerce_API.Core.Contracts;
using ECommerce_API.Core.Models.Category;
using ECommerce_API.Core.Models.Product;

namespace ECommerce_API.Core.Services
{
    public class ProductCategoryServices
    {

        private readonly ICategoriesRepositroy _categoriesRepositroy;
        private readonly IProductsRepository _productsRepository;

        public ProductCategoryServices(ICategoriesRepositroy categoriesRepositroy, IProductsRepository productsRepository)
        {
            this._categoriesRepositroy = categoriesRepositroy;
            this._productsRepository = productsRepository;
        }

        public async Task<List<BaseProductDto>> SearchProducts(string searchString)
        {
            var products = await _productsRepository.SearchParameters<BaseProductDto>(searchString);

            return products;
        }
    }
}

using ECommerce_API.Core.Contracts;
using ECommerce_API.Core.Models.Category;
using ECommerce_API.Core.Models.Product;


using ECommerce_API.Exceptions;

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
            if (string.IsNullOrEmpty(searchString))
            {
                throw new BadRequestException(
                        "SearchProducts SearchString is Empty", nameof(SearchProducts)
                );
            }
            var products = await _productsRepository.SearchParameters<BaseProductDto>(searchString);
            if (products == null || products.Count() == 0)
            {
                throw new NotFoundException(
                    "SearchProducts No Items" , nameof(SearchProducts)
                );
            } 
            return products;
        }
    }
}

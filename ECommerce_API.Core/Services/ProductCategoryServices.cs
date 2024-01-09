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

            var categories = await _categoriesRepositroy.SearchParameters<BaseCategoryDto>(searchString);

            var productsAssociated = await _productsRepository.getProductsAssociated(categories);

            // dont add dupplicated products
            List<BaseProductDto> productsResult;
            if (products.Count > 0)
            {
                productsResult = products;
            }
            else
            {
                productsResult = new List<BaseProductDto>();
            }

            for (int i = 0; i < productsAssociated.Count; i++)
            {
                if (!productsResult.Contains(productsAssociated[i]))
                {
                    productsResult.Add(productsAssociated[i]);
                }
            }



            return productsResult;
        }
    }
}

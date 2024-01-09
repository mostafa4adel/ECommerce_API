using ECommerce_API.Core.Contracts;
using ECommerce_API.Core.Models;
using ECommerce_API.Core.Models.Category;
using ECommerce_API.Core.Models.Product;


namespace ECommerce_API.Core.Services
{
    public class CategoriesServices
    {
        private readonly ICategoriesRepositroy _categoriesRepositroy;
        private readonly IProductsRepository _productsRepository;
        public CategoriesServices(ICategoriesRepositroy categoriesRepositroy , IProductsRepository productsRepository) {
        
            this._categoriesRepositroy = categoriesRepositroy;
            this._productsRepository = productsRepository;
        }


        

        public async Task<PagedResult<BaseCategoryDto>> getAsyncAllCategories(QueryParameters queryParameters)
        {
            var categories = await _categoriesRepositroy.GetAllAsync<BaseCategoryDto>(queryParameters);

            return categories;
        }

        
        public async Task<BaseCategoryDto> GetCategoryById(int id)
        {
            var category = await _categoriesRepositroy.GetAsync<BaseCategoryDto>(id);

            return category;
        }

        
    }
}

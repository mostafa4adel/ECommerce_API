
using ECommerce_API.Core.Models.Category;
using ECommerce_API.Core.Models.Product;
using ECommerce_API.Data;

namespace ECommerce_API.Core.Contracts
{
    public interface IProductsRepository : IGenericRepository<Product>
    {
        public Task<List<BaseProductDto>> getProductsAssociated(List<BaseCategoryDto> baseCategories);
    }
}

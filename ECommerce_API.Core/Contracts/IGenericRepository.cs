using ECommerce_API.Core.Models;

namespace ECommerce_API.Core.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        
        Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters q);

        Task<TResult> GetAsync<TResult>(int? id);

        Task<List<TResult>> SearchParameters<TResult>(string parameter);

    }
}

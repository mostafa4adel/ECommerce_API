using ECommerce_API.Core.Contracts;
using ECommerce_API.Data;
using AutoMapper;
using ECommerce_API.Core.Models;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_API.Core.Repository
{
    public class GenericRepository<T>:IGenericRepository<T> where T:class
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;

        public GenericRepository(DatabaseContext databaseContext, IMapper mapper) {
            this._context = databaseContext;
            this._mapper = mapper;
        }

        public async Task<PagedResult<TResult>> GetAllAsync<TResult>(QueryParameters q)
        {
            
            var totalSize = await _context.Set<T>().CountAsync();
            

            var items = await _context.Set<T>()
                .Skip(q.StartIndex)
                .Take(q.PageSize)
                .ProjectTo<TResult>(_mapper.ConfigurationProvider)
                .ToListAsync();

            

            return new PagedResult<TResult>
            {
                Items = items,
                PageNumber = q.PageNumber,
                RecordNumber = q.PageSize,
                TotalCount = totalSize
            };
        }

        public virtual async Task<TResult> GetAsync<TResult>(int? id)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<TResult>> SearchParameters<TResult>(string parameter)
        {
            throw new NotImplementedException();
        }
    }
}

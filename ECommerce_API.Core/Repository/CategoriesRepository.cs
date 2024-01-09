using AutoMapper;
using AutoMapper.QueryableExtensions;
using ECommerce_API.Core.Contracts;
using ECommerce_API.Core.Models.Category;
using ECommerce_API.Data;
using Microsoft.EntityFrameworkCore;

namespace ECommerce_API.Core.Repository
{
    public class CategoriesRepository : GenericRepository<Category>, ICategoriesRepositroy
    {
        private readonly DatabaseContext _context;
        private readonly IMapper _mapper;
        public CategoriesRepository(DatabaseContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        

        public override async Task<BaseCategoryDetails> GetAsync<BaseCategoryDetails>(int? id)
        {
            var categoryDto = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id);
            return _mapper.Map<BaseCategoryDetails>(categoryDto);

        }


        public override async Task<List<BaseCategoryDto>> SearchParameters<BaseCategoryDto>(string parameter)

        {
            var items = await _context.Categories
                            .Where(x => x.CategoryName.Contains(parameter))
                            .ProjectTo<BaseCategoryDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();

            return items;
        }
    }

}

using AutoMapper;
using ECommerce_API.Core.Contracts;
using ECommerce_API.Core.Models.Category;
using ECommerce_API.Data;
using Microsoft.EntityFrameworkCore;
using ECommerce_API.Core.Models.Product;
using System.Reflection.Metadata;
using AutoMapper.QueryableExtensions;

namespace ECommerce_API.Core.Repository
{
    public class ProductsRepository : GenericRepository<Product> , IProductsRepository
    {
        private readonly DatabaseContext  _context;
        private readonly IMapper _mapper;
        
        public ProductsRepository(DatabaseContext context, IMapper mapper) : base(context, mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }


        public override async Task<ProductDetailsDto> GetAsync<ProductDetailsDto>(int? id)
        {
            // get the product with the given id
            // get the catigories that this product has in the many to many relationship
            var product = await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id);          
            var productDetailsDto = _mapper.Map<ProductDetailsDto>(product);
            return productDetailsDto;
        }

        public async Task<List<BaseProductDto>> getProductsAssociated(List <BaseCategoryDto> baseCategories)
        {
           var categoryIds = baseCategories.Select(c => c.Id).ToList();

            var productsIDs = await  _context.productCategories
                .Where(pc => categoryIds.Any(c => c == pc.CategoryId))
                .Select(pc => pc.ProductId)
                .ToListAsync();


            var products = await _context.Products
                .Where(pc => productsIDs.Any(p => p ==  pc.Id))
                .ToListAsync();

            var baseProducts = _mapper.Map<List<BaseProductDto>>(products);
            
            return baseProducts;
        }

       

        public override async Task<List<BaseProductDto>> SearchParameters<BaseProductDto>(string parameter)

        {
            var  items = await _context.Products
                            .Where(x => x.ProductName.Contains(parameter))
                            .ProjectTo<BaseProductDto>(_mapper.ConfigurationProvider)
                            .ToListAsync();

            return items;
        }
            
    }
}

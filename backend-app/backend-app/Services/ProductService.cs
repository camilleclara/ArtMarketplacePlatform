using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;
using backend_app.Models.Enums;
using backend_app.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Services
{
    public class ProductService : IProductService
    {
        private readonly MarketPlaceContext _context;
        private readonly IMapper _mapper;

        public ProductService(MarketPlaceContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _context.Products
                .Include(p=>p.Artisan)
                .ToListAsync();
                
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetById(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetByCategory(Category category)
        {
            string categoryString = category.ToString();

            //Filtrer les produits par catégorie
            var products = await _context.Products
                                         .Where(p => p.Category == categoryString)
                                         .ToListAsync();

            //Mapper les résultats en DTO
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }


        public async Task<IEnumerable<ProductDTO>> GetByArtisanId(int artisanId)
        {
            var products = await _context.Products
                                         .Where(p => p.ArtisanId == artisanId)
                                         .ToListAsync();

            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> UpdateAsync(int id, ProductDTO product)
        {
            Product p = await _context.Products.FindAsync(id);
            if(p == null)
            {
                return null;
                //TODO throw Not Found Exception
            }

            _mapper.Map(product, p);
            _context.Products.Update(p);
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(p);

        }

        public async Task<ProductDTO> AddAsync(ProductDTO product)
        {
            if(product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            await _context.Products.AddAsync(_mapper.Map<Product>(product));
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(product);
        }
    }
}

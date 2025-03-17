using DAL.Repositories.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly MarketPlaceContext _context;

        public ProductRepository(MarketPlaceContext context)
        {
            _context = context;
        }
        public void Delete(Product product)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteById(int id)
        {
            var result = await _context.Products
                                     .Where(p => p.Id == id)
                                     .ExecuteDeleteAsync();
            return result;

            //Soft delete
            //Product product = await _context.Products.FindAsync(id);

            //if (product == null || !product.IsActive)
            //{
            //    // Le produit n'existe pas ou est déjà marqué comme supprimé
            //    throw new ArgumentException("No product was found with this id.");
            //    //TODO throw exception
            //}
            //// Marquer comme supprimé en définissant IsDeleted à true
            //product.IsActive = false;
            //// Mettre à jour dans la base de données
            //_context.Products.Update(product);
            //await _context.SaveChangesAsync();


        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products
                .Include(p => p.Artisan)
                .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.Include(i => i.ProductImages).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> Insert(Product entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Product> Update(int id, Product entityUpdated)
        {
            var storedProduct = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (storedProduct == null)
            {
                throw new ArgumentException();//TODO throw error not found
            }
            storedProduct.Name = entityUpdated.Name;
            storedProduct.Description = entityUpdated.Description;
            storedProduct.Price = entityUpdated.Price;
            storedProduct.Category = entityUpdated.Category;
            storedProduct.IsAvailable = entityUpdated.IsAvailable;
            //TODO update images
            //TODO; update chats and other collections
            _context.Products.Update(storedProduct);
            await _context.SaveChangesAsync();


            return (storedProduct);
        }

        public async Task<IEnumerable<Product>> GetByCategory(String category)
        {
            var storedProductsFromCategory = await _context.Products
                                         .Where(p => p.Category == category)
                                         .ToListAsync();
            return storedProductsFromCategory;
        }

        public async Task<IEnumerable<Product>> GetByAttributeId(int id)
        {
            var storedProductsForArtisanId = await _context.Products
                                         .Where(p => p.ArtisanId == id)
                                         .Where(p => p.IsActive)
                                         .ToListAsync();

            return storedProductsForArtisanId;
        }

        public async Task<Product> SoftDeleteById(int id)
        {
            var storedProduct = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (storedProduct == null)
            {
                throw new ArgumentException();//TODO throw error not found
            }
            storedProduct.IsActive = false;
            //TODO; update chats and other collections
            _context.Products.Update(storedProduct);
            await _context.SaveChangesAsync();


            return (storedProduct);
        }
    }
}

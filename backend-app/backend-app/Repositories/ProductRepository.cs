using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;
using backend_app.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend_app.Repositories
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
            return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
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
            storedProduct.ArtisanId = entityUpdated.ArtisanId;
            storedProduct.Name = entityUpdated.Name;
            storedProduct.Description = entityUpdated.Description;
            storedProduct.Price = entityUpdated.Price;
            storedProduct.Category = entityUpdated.Category;
            storedProduct.IsActive = entityUpdated.IsActive;
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
                                         .ToListAsync();

            return storedProductsForArtisanId;
        }
    }
}

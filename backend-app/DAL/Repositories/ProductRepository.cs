﻿using DAL.Repositories.Interfaces;
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
           
            await _context.ProductImages
                  .Where(img => img.ProductId == id)
                  .ExecuteDeleteAsync();

            // Supprimer le produit ensuite
            var result = await _context.Products
                                       .Where(p => p.Id == id)
                                       .ExecuteDeleteAsync();

            return result;


        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            return await _context.Products
                .Where(p=>p.IsActive)
                .Include(p => p.Artisan)
                .Include(p => p.ProductImages)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetAllAdmin()
        {
            return await _context.Products
                .Include(p => p.Artisan)
                .Include(p => p.ProductImages)
                .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products
                .Where(p => p.IsActive)
                .Include(i => i.ProductImages)
                .Include(i => i.Reviews).ThenInclude(r=> r.Customer)
                .Include(p => p.Artisan)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Product> Insert(Product entity)
        {
            await _context.Products.AddAsync(entity);
            //await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Product> Update(int id, Product entityUpdated)
        {
            var storedProduct = await _context.Products
                .Where(p => p.Id == id)
                .Include(p => p.ProductImages)
                .FirstOrDefaultAsync();
            if (storedProduct == null)
            {
                throw new ArgumentException();//TODO throw error not found
            }
            storedProduct.Name = entityUpdated.Name;
            storedProduct.Description = entityUpdated.Description;
            storedProduct.Price = entityUpdated.Price;
            storedProduct.Category = entityUpdated.Category;
            storedProduct.IsAvailable = entityUpdated.IsAvailable;

            if (entityUpdated.ProductImages != null)
            {
                // Étape 1: Conserver les images existantes que nous voulons garder
                var existingImageIds = entityUpdated.ProductImages
                    .Where(img => img.Id > 0)
                    .Select(img => img.Id)
                    .ToList();

                var imagesToRemove = storedProduct.ProductImages
                    .Where(img => !existingImageIds.Contains(img.Id))
                    .ToList();

                foreach (var imageToRemove in imagesToRemove)
                {
                    storedProduct.ProductImages.Remove(imageToRemove);
                    _context.Entry(imageToRemove).State = EntityState.Deleted;
                }
                var newImages = entityUpdated.ProductImages.Where(img => img.Id == 0).ToList();

                foreach (var newImage in newImages)
                {
                    newImage.ProductId = id;
                    storedProduct.ProductImages.Add(newImage);
                }
            }
            _context.Products.Update(storedProduct);
            return (storedProduct);
        }

        public async Task<IEnumerable<Product>> GetByCategory(String category)
        {
            var storedProductsFromCategory = await _context.Products
                                         .Where(p => p.Category == category)
                                         .Include(i => i.ProductImages)
                                         .ToListAsync();
            return storedProductsFromCategory;
        }

        public async Task<IEnumerable<Product>> GetByAttributeId(int id)
        {
            var storedProductsForArtisanId = await _context.Products
                                         .Where(p => p.ArtisanId == id)
                                         .Where(p => p.IsActive)
                                         .Include(i => i.ProductImages)
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
            await SaveChanges();
            //await _context.SaveChangesAsync();


            return (storedProduct);
        }

        public async Task<Product> ApproveById(int id)
        {
            var storedProduct = await _context.Products.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (storedProduct == null)
            {
                throw new ArgumentException();//TODO throw error not found
            }
            storedProduct.IsActive = true;
            _context.Products.Update(storedProduct);
            await SaveChanges();
            return (storedProduct);
        }



        public async Task SaveChanges()
        {
            //TODO test and repeat for all entities: a save changes method, so changes are saved only once, called by the service
            await _context.SaveChangesAsync();
        }

        public async Task<List<int>> GetReviewableProductIdsByCustomerId(int customerId)
        {
            return await _context.Orders
                 .Where(o => o.CustomerId == customerId &&
                             o.Deliveries.Any(d => d.DeliStatus == "DELIVERED"))
                 .SelectMany(o => o.ItemOrders)
                 .Where(io => io.ProductId != null)
                 .Select(io => io.ProductId.Value)
                 .Distinct()
                 .ToListAsync();
        }
    }
}

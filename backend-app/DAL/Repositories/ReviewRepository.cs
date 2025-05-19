using DAL.Repositories.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly MarketPlaceContext _context;

        public ReviewRepository(MarketPlaceContext context)
        {
            _context = context;
        }
        public void Delete(Review review)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteById(int id)
        {
            var result = await _context.Reviews
                                     .Where(p => p.Id == id)
                                     .ExecuteDeleteAsync();
            return result;

            //Soft delete
            //Review review = await _context.Reviews.FindAsync(id);

            //if (review == null || !review.IsActive)
            //{
            //    // Le produit n'existe pas ou est déjà marqué comme supprimé
            //    throw new ArgumentException("No review was found with this id.");
            //    //TODO throw exception
            //}
            //// Marquer comme supprimé en définissant IsDeleted à true
            //review.IsActive = false;
            //// Mettre à jour dans la base de données
            //_context.Reviews.Update(review);
            //await _context.SaveChangesAsync();


        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            return await _context.Reviews
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .ToListAsync();
        }

        public async Task<Review> GetById(int id)
        {
            return await _context.Reviews
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Review> Insert(Review entity)
        {
            entity.IsActive = true;
            await _context.Reviews.AddAsync(entity);
            return entity;
        }
        public async Task<Review> Update(int id, Review entityUpdated)
        {
            var storedReview = await _context.Reviews
                .Where(p => p.Id == id)
                .Include(r => r.Customer)
                .Include(r => r.Product)
                .FirstOrDefaultAsync();
            if (storedReview == null)
            {
                throw new ArgumentException();
            }
            storedReview.ProductId = entityUpdated.ProductId;
            storedReview.CustomerId = entityUpdated.CustomerId;
            storedReview.Content = entityUpdated.Content;
            storedReview.FromArtisan = entityUpdated.FromArtisan;
            storedReview.IsActive = entityUpdated.IsActive;
            storedReview.Score = entityUpdated.Score;
            _context.Reviews.Update(storedReview);
            return (storedReview);
        }
        public async Task<IEnumerable<Review>> GetByProductId(int id)
        {
            var storedReviewsForArtisanId = await _context.Reviews
                                         .Where(r => r.ProductId == id)
                                         .Where(r => r.IsActive)
                                         .Include(r => r.Customer)
                                         .Include(r => r.Product)
                                         .ToListAsync();

            return storedReviewsForArtisanId;
        }

        public async Task<IEnumerable<Review>> GetByArtisanId(int id)
        {
            var storedReviewsForArtisanId = await _context.Reviews
                                         .Include(r => r.Customer)
                                         .Include(r => r.Product)
                                         .Where(r => r.Product.ArtisanId == id)
                                         .Where(r => r.IsActive)
                                         .ToListAsync();

            return storedReviewsForArtisanId;
        }

        public async Task<Review> SoftDeleteById(int id)
        {
            var storedReview = await _context.Reviews.Where(p => p.Id == id).FirstOrDefaultAsync();
            if (storedReview == null)
            {
                throw new ArgumentException();//TODO throw error not found
            }
            storedReview.IsActive = false;
            //TODO; update chats and other collections
            _context.Reviews.Update(storedReview);
            return (storedReview);
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

    }
}

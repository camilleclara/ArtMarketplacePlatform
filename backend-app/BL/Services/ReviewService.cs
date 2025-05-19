using AutoMapper;
using BL.Models;
using BL.Models.Enums;
using BL.Services.Interfaces;
using DAL.Repositories.Interfaces;
using Domain;
namespace BL.Services
{
    public class ReviewService : IReviewService
    {

        private readonly IReviewRepository _repository;
        private readonly IMapper _mapper;


        public ReviewService(IReviewRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<ReviewDTO>> GetAllAsync()
        {
            var reviews = await _repository.GetAll();
            var reviewDTOS = _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
            return reviewDTOS;
        }

        public async Task<ReviewDTO> GetByIdAsync(int id)
        {
            var review = await _repository.GetById(id);
            return _mapper.Map<ReviewDTO>(review);
        }

        public async Task<ReviewDTO> AddAsync(ReviewDTO newReview)
        {
            var newReviewEntity = _mapper.Map<Review>(newReview);
            await _repository.Insert(newReviewEntity);
            await _repository.SaveChanges();
            return _mapper.Map<ReviewDTO>(newReview);
        }

        public async Task<ReviewDTO> UpdateAsync(int id, ReviewDTO reviewToUpdate)
        {
            //Mapper les properties de base
            var reviewToUpdateEntity = _mapper.Map<Review>(reviewToUpdate);

            await _repository.Update(id, reviewToUpdateEntity);
            await _repository.SaveChanges();
            return _mapper.Map<ReviewDTO>(reviewToUpdateEntity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            try {
                await _repository.SoftDeleteById(id);
                await _repository.SaveChanges();
                return 1;
            }
            catch (Exception ex) { return 0; }
        }


        public async Task<IEnumerable<ReviewDTO>> GetByArtisanId(int artisanId)
        {
            var reviewsEntities = await _repository.GetByArtisanId(artisanId);
            return _mapper.Map<IEnumerable<ReviewDTO>>(reviewsEntities);
        }

        public async Task<IEnumerable<ReviewDTO>> GetByProductId(int artisanId)
        {
            var reviewsEntities = await _repository.GetByProductId(artisanId);
            return _mapper.Map<IEnumerable<ReviewDTO>>(reviewsEntities);
        }

        public async Task<ReviewDTO> AddAsyncForProduct(ReviewDTO review, int productId)
        {
            review.ProductId = productId;
            return await AddAsync(review);
        }
    }
}

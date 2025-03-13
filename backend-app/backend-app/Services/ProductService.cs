using AutoMapper;
using backend_app.Models;
using backend_app.Models.DTO;
using backend_app.Models.Enums;
using backend_app.Repositories;
using backend_app.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace backend_app.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _repository.GetAll();
            var productDTOS = _mapper.Map<IEnumerable<ProductDTO>>(products);
            return productDTOS;
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var product = await _repository.GetById(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> AddAsync(ProductDTO newProduct)
        {
            var newProductEntity = _mapper.Map<Product>(newProduct);
            await _repository.Insert(newProductEntity);
            return _mapper.Map<ProductDTO>(newProduct);
        }

        public async Task<ProductDTO> UpdateAsync(int id, ProductDTO productToUpdate)
        {
            var productToUpdateEntity = _mapper.Map<Product>(productToUpdate);
            await _repository.Update(id, productToUpdateEntity);
            return _mapper.Map<ProductDTO>(productToUpdateEntity);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _repository.DeleteById(id);
        }

        public async Task<IEnumerable<ProductDTO>> GetByCategoryAsync(Category category)
        {
            string categoryString = category.ToString();

            //Filtrer les produits par catégorie
            var productsEntities = await _repository.GetByCategory(categoryString);
            var productsDTO = _mapper.Map<IEnumerable<ProductDTO>>(productsEntities);

            //Mapper les résultats en DTO
            return productsDTO;
        }


        public async Task<IEnumerable<ProductDTO>> GetByArtisanId(int artisanId)
        {
            var productsEntities = _repository.GetByAttributeId(artisanId);
            return _mapper.Map<IEnumerable<ProductDTO>>(productsEntities);
        }

        public Task<ProductDTO> AddAsyncForArtisan(ProductDTO product, int artisanId)
        {
            product.ArtisanId = artisanId;
            return AddAsync(product);
        }
    }
}

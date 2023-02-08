using LymStyleWebAPPAPI.Domain.Contracts.Repositories;
using LymStyleWebAPPAPI.Domain.Contracts.Services;
using LymStyleWebAPPAPI.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LymStyleWebAPPAPI.Application.Service.SQLServerServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<int> Delete(int id)
        {
            var entity = await _productRepository.GetById(id);
            return await _productRepository.Delete(entity);
        }
        public List<ProductDTO> GetAll()
        {
            return _productRepository.GetAll().Select(product => new ProductDTO()
            {
                id = product.Id,
                name = product.Name,
                categoryId = product.CategoryId,
                description = product.Description,
                price = product.Price,
                image = product.Image
            }).ToList();
        }
        public async Task<ProductDTO> GetById(int id)
        {
            var dto = new ProductDTO();
            return dto.mapToDTO(await _productRepository.GetById(id));
        }

        public Task<int> Save(ProductDTO dto)
        {
            if (dto.id > 0)
            {
                return _productRepository.Update(dto.mapToEntity());
            }
            else
            {
                return _productRepository.Save(dto.mapToEntity());
            }
        }

        public Task<int> SaveFile(int id, string filename)
        {
           return _productRepository.SaveFile(id, filename);
        }
    }
}

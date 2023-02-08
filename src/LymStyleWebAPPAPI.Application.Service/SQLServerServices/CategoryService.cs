using LymStyleWebAPPAPI.Domain.Contracts.Repositories;
using LymStyleWebAPPAPI.Domain.Contracts.Services;
using LymStyleWebAPPAPI.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LymStyleWebAPPAPI.Application.Service.SQLServerServices
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<int> Delete(int id)
        {
            var entity = await _categoryRepository.GetById(id);
            return await _categoryRepository.Delete(entity);
        }
        public List<CategoryDTO> GetAll()
        {
            return _categoryRepository.GetAll()
                             .Select(category => new CategoryDTO()
                             {
                                 id = category.Id,
                                 name = category.Name,
                             }).ToList();
        }
        public async Task<CategoryDTO> GetById(int id)
        {
            var dto = new CategoryDTO();
            return dto.maptoDTO(await _categoryRepository.GetById(id));
        }
        public Task<int> Save(CategoryDTO entity)
        {
            if(entity.id > 0)
            {
                return _categoryRepository.Update(entity.mapToEntity());
            }
            else
            {
                return _categoryRepository.Save(entity.mapToEntity());
            }
        }

        public Task<int> SaveFile(int id, string filename)
        {
            return _categoryRepository.SaveFile(id, filename);
        }
    }
}

using AutoMapper;
using BLL.Interfaces;
using BLL.ModelsDTO;
using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class CategoryService : IService<CategoryDTO>
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService()
        {
            EFAppContext context = new EFAppContext();
            _categoryRepository = new CategoryRepository(context);
        }

        public async Task<int> Create(CategoryDTO item)
        {
            if (item != null)
            {
                var entity = MappingToEntity(item);
                await _categoryRepository.Create(entity);
                return entity.Id;
            }
            return 0;
        }

        public async Task Delete(int? id)
        {
            if (id != null)
            {
               await _categoryRepository.Delete((int)id);
            };
        }

        public async Task<CategoryDTO> Find(int? id)
        {
            if (id != null)
            {
                CategoryEntity item = await _categoryRepository.GetById((int)id);
                return MappingToDTO(item);
            }
                return null;
        }
        
        public IList<CategoryDTO> GetAll()
        {
            var list = new List<CategoryDTO>();
            foreach (var cat in _categoryRepository.GetAll())
            {
                list.Add(MappingToDTO(cat));
            }
            return list;
        }

        public async Task Update(int id,CategoryDTO item)
        {
            if (item != null)
            {
                await _categoryRepository.Update(id, MappingToEntity(item));
            }
        }

        private CategoryEntity MappingToEntity(CategoryDTO categoryDTO)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<CategoryDTO, CategoryEntity>()).CreateMapper();
            if (categoryDTO != null)
                return translateObj.Map<CategoryDTO, CategoryEntity>(categoryDTO);

            return null;
        }

        private CategoryDTO MappingToDTO(CategoryEntity categoryEntity)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<CategoryEntity, CategoryDTO>()).CreateMapper();

            if (categoryEntity != null)
                return translateObj.Map<CategoryEntity, CategoryDTO>(categoryEntity);

            return null;
        }
    }
}

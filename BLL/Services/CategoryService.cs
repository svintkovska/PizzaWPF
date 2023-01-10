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

        public void Create(CategoryDTO item)
        {
            if (item != null)
            {
                _categoryRepository.Create(TranslateCategoryDTOToCategoryEntity(item));
            }
        }

        public void Delete(int? id)
        {
            if (id != null)
            {
                _categoryRepository.Delete((int)id);
            };
        }

        public CategoryDTO Find(int? id)
        {
            if (id != null)
            {
                Task<CategoryEntity> item = _categoryRepository.GetById((int)id);
                return TranslateCategoryEntityToCategoryDTO(item.Result);
            }
                return null;
        }
        
        public IList<CategoryDTO> GetAll()
        {
            var list = new List<CategoryDTO>();
            foreach (var cat in _categoryRepository.GetAll())
            {
                list.Add(TranslateCategoryEntityToCategoryDTO(cat));
            }
            return list;
        }

        public void Update(int id,CategoryDTO item)
        {
            if (item != null)
            {
                _categoryRepository.Update(id, TranslateCategoryDTOToCategoryEntity(item));
            }
        }

        private CategoryEntity TranslateCategoryDTOToCategoryEntity(CategoryDTO categoryDTO)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<CategoryDTO, CategoryEntity>()).CreateMapper();
            if (categoryDTO != null)
                return new CategoryEntity()
                {
                    Id = categoryDTO.Id,
                    Name = categoryDTO.Name,
                    Image = categoryDTO.Image,
                    DateCreated = categoryDTO.DateCreated,
                    IsDelete = categoryDTO.IsDelete
                };
            return null;
        }

        private CategoryDTO TranslateCategoryEntityToCategoryDTO(CategoryEntity categoryEntity)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<CategoryEntity, CategoryDTO>()).CreateMapper();

            if (categoryEntity != null)
                return new CategoryDTO()
                {
                    Id = categoryEntity.Id,
                    Name = categoryEntity.Name,
                    Image = categoryEntity.Image,
                    DateCreated = categoryEntity.DateCreated,
                    IsDelete = categoryEntity.IsDelete
                };
            return null;
        }
    }
}

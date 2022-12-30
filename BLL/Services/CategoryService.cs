using AutoMapper;
using BLL.Interfaces;
using BLL.ModelsDTO;
using DAL.Data.Entities;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
           
namespace BLL.Services
{
    public class CategoryService : IService<CategoryDTO>
    {
        private readonly CategoryRepository _categoryRepository = new CategoryRepository();

        public void Create(CategoryDTO item)
        {
            if (item != null)
            {
                var cat = TranslateCategoryDTOToCategoryEntity(item);
                _categoryRepository.Create(cat);
                item.Id = cat.Id;
            }
        }

        public void Delete(int? id)
        {
            if (id != null)
            {
                _categoryRepository.Delete(id);
            }
        }

        public CategoryDTO Find(int? id)
        {
            if (id != null)
                return TranslateCategoryEntityToCategoryDTO(_categoryRepository.Find(id));
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

        public void Update(CategoryDTO item)
        {
            if (item != null)
            {
                _categoryRepository.Update(TranslateCategoryDTOToCategoryEntity(item));
            }
        }

        private CategoryEntity TranslateCategoryDTOToCategoryEntity(CategoryDTO categoryDTO)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<CategoryDTO, CategoryEntity>()).CreateMapper();
            if (categoryDTO != null)
                return new CategoryEntity()
                {
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

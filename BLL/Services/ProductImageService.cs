using AutoMapper;
using BLL.Interfaces;
using BLL.ModelsDTO;
using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductImageService : IService<ProductImageDTO>
    {
        private readonly IProductImagesRepository _productImgRepository;
        public ProductImageService()
        {
            EFAppContext context = new EFAppContext();
            _productImgRepository = new ProductImageRepository(context);
        }
        public async Task<int> Create(ProductImageDTO item)
        {
            if (item != null)
            {
                var entity = MappingToEntity(item);
                await _productImgRepository.Create(entity);
                return entity.Id;
            }
            return 0;
        }

        public async Task Delete(int? id)
        {
            if (id != null)
            {
               await _productImgRepository.Delete((int)id);
            };
        }

        public async Task <ProductImageDTO> Find(int? id)
        {
            if (id != null)
            {
                ProductImageEntity item = await _productImgRepository.GetById((int)id);
                return MappingToDTO(item);
            }
            return null;
        }

        public IList<ProductImageDTO> GetAll()
        {
            var list = new List<ProductImageDTO>();
            foreach (var cat in _productImgRepository.GetAll())
            {
                list.Add(MappingToDTO(cat));
            }
            return list;
        }

        public async Task Update(int id, ProductImageDTO item)
        {
            if (item != null)
            {
               await _productImgRepository.Update(id, MappingToEntity(item));
            }
        }

        private ProductImageEntity MappingToEntity(ProductImageDTO productImgDTO)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<ProductImageDTO, ProductImageEntity>()).CreateMapper();
            if (productImgDTO != null)
                return translateObj.Map<ProductImageDTO, ProductImageEntity>(productImgDTO);

            return null;
        }

        private ProductImageDTO MappingToDTO(ProductImageEntity productImgEntity)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<ProductImageEntity, ProductImageDTO>()).CreateMapper();

            if (productImgEntity != null)
                return translateObj.Map<ProductImageEntity, ProductImageDTO>(productImgEntity);
            return null;
        }
    }
}

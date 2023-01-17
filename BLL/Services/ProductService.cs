using AutoMapper;
using BLL.Interfaces;
using BLL.ModelsDTO;
using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService : IService<ProductDTO>
    {
        private readonly IProductRepository _productRepository;
        public ProductService()
        {
            EFAppContext context = new EFAppContext();
            _productRepository = new ProductRepository(context);

        }
        public async Task<int> Create(ProductDTO item)
        {
            if (item != null)
            {
                var entity = MappingToEntity(item);
                await _productRepository.Create(entity);
                var id = entity.Id;
                return id;
            }
            return 0;
        }

        public async Task Delete(int? id)
        {
            if (id != null)
            {
                await _productRepository.Delete((int)id);
            };
        }

        public async Task<ProductDTO> Find(int? id)
        {
            if (id != null)
            {
                ProductEntity item = await _productRepository.GetById((int)id);
                return MappingToDTO(item);
            }
            return null;
        }

        public IList<ProductDTO> GetAll()
        {
            var list = new List<ProductDTO>();
            foreach (var prod in _productRepository.GetAll())
            {
                list.Add(MappingToDTO(prod));
            }
            return list;
        }

        public async Task Update(int id, ProductDTO item)
        {
            if (item != null)
            {
                await _productRepository.Update(id, MappingToEntity(item));
            }
        }

        private ProductEntity MappingToEntity(ProductDTO productDTO)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<ProductDTO, ProductEntity>()).CreateMapper();
            if (productDTO != null)
                return translateObj.Map<ProductDTO, ProductEntity>(productDTO);

            return null;
        }

        private ProductDTO MappingToDTO(ProductEntity productEntity)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<ProductEntity, ProductDTO>()).CreateMapper();
            if (productEntity != null)
                return translateObj.Map<ProductEntity, ProductDTO>(productEntity);

            return null;
        }
    }
}

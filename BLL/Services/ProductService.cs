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
                var entity = TranslateProductDTOToProductEntity(item);
                await _productRepository.Create(entity);
                var id = entity.Id;
                return id;
            }
            return 0;
        }

        public void Delete(int? id)
        {
            if (id != null)
            {
                _productRepository.Delete((int)id);
            };
        }

        public ProductDTO Find(int? id)
        {
            if (id != null)
            {
                Task<ProductEntity> item = _productRepository.GetById((int)id);
                return TranslateProductEntityToProductDTO(item.Result);
            }
            return null;
        }

        public IList<ProductDTO> GetAll()
        {
            var list = new List<ProductDTO>();
            foreach (var prod in _productRepository.GetAll())
            {
                list.Add(TranslateProductEntityToProductDTO(prod));
            }
            return list;
        }

        public void Update(int id, ProductDTO item)
        {
            if (item != null)
            {
                _productRepository.Update(id, TranslateProductDTOToProductEntity(item));
            }
        }

        private ProductEntity TranslateProductDTOToProductEntity(ProductDTO productDTO)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<ProductDTO, ProductEntity>()).CreateMapper();
            if (productDTO != null)
                return new ProductEntity()
                {
                    Id = productDTO.Id,
                    Name = productDTO.Name,
                    Price = productDTO.Price,
                    DiscountPrice = productDTO.DiscountPrice,
                    IsOnDiscount = productDTO.IsOnDiscount,
                    Weight = productDTO.Weight,
                    Description = productDTO.Description,
                    CategoryId = productDTO.CategoryId,
                    DateCreated = productDTO.DateCreated,
                    IsDelete = productDTO.IsDelete
                };
            return null;
        }

        private ProductDTO TranslateProductEntityToProductDTO(ProductEntity productEntity)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<ProductEntity, ProductDTO>()).CreateMapper();
            if (productEntity != null)
                return new ProductDTO()
                {
                    Id = productEntity.Id,
                    Name = productEntity.Name,
                    Price = productEntity.Price,
                    DiscountPrice = productEntity.DiscountPrice,
                    IsOnDiscount = productEntity.IsOnDiscount,
                    Weight = productEntity.Weight,
                    Description = productEntity.Description,
                    CategoryId = productEntity.CategoryId,
                    DateCreated = productEntity.DateCreated,
                    IsDelete = productEntity.IsDelete
                };
            return null;
        }
    }
}

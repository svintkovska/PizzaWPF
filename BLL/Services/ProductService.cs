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
    public class ProductService : IService<ProductDTO>
    {
        private readonly ProductRepository _productRepository = new ProductRepository();

        public void Create(ProductDTO item)
        {
            if (item != null)
            {
                var prod = TranslateProductDTOToProductEntity(item);
                _productRepository.Create(prod);
                item.Id = prod.Id;
            }
        }

        public void Delete(int? id)
        {
            if (id != null)
            {
                _productRepository.Delete(id);
            }
        }

        public ProductDTO Find(int? id)
        {
            if (id != null)
                return TranslateProductEntityToProductDTO(_productRepository.Find(id));
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

        public void Update(ProductDTO item)
        {
            if (item != null)
            {
                _productRepository.Update(TranslateProductDTOToProductEntity(item));
            }
        }

        private ProductEntity TranslateProductDTOToProductEntity(ProductDTO productDTO)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<ProductDTO, ProductEntity>()).CreateMapper();
            if (productDTO != null)
                return new ProductEntity()
                {
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

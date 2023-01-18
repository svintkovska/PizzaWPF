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
    public class UserService : IService<UserDTO>
    {
        private readonly IUserRepository _userRepository;
        public UserService()
        {
            EFAppContext context = new EFAppContext();
            _userRepository = new UserRepository(context);
        }
        public async Task<int> Create(UserDTO item)
        {
            if (item != null)
            {
                var entity = MappingToEntity(item);
                await _userRepository.Create(entity);
                return entity.Id;
            }
            return 0;
        }

        public async Task Delete(int? id)
        {
            if (id != null)
            {
                await _userRepository.Delete((int)id);
            }; 
        }

        public async Task<UserDTO> Find(int? id)
        {
            if (id != null)
            {
                UserEntity item = await _userRepository.GetById((int)id);
                return MappingToDTO(item);
            }
            return null;
        }

        public IList<UserDTO> GetAll()
        {
            var list = new List<UserDTO>();
            foreach (var user in _userRepository.GetAll())
            {
                list.Add(MappingToDTO(user));
            }
            return list;
        }

        public async Task Update(int id, UserDTO item)
        {
            if (item != null)
            {
                await _userRepository.Update(id, MappingToEntity(item));
            }
        }

        public async Task AddProductToBasket(BasketDTO basketDTO)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<BasketDTO, BasketEntity>()).CreateMapper();
            var basket = translateObj.Map<BasketDTO, BasketEntity>(basketDTO);
            await _userRepository.AddProductToBasket(basket);
        }
        public async Task UpdateProductInBasket(BasketDTO basketDTO)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<BasketDTO, BasketEntity>()).CreateMapper();
            var basket = translateObj.Map<BasketDTO, BasketEntity>(basketDTO);
            basket.Product = null;
            basket.User = null;
            await _userRepository.UpdateProductInBasket(basket);
        }
        public async Task RemoveProductFromBasket(BasketDTO basketDTO)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<BasketDTO, BasketEntity>()).CreateMapper();
            var basket = translateObj.Map<BasketDTO, BasketEntity>(basketDTO);
            basket.Product = null;
            basket.User = null;
            await _userRepository.RemoveProductFromBasket(basket);
        }


        private UserEntity MappingToEntity(UserDTO userDTO)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<UserDTO, UserEntity>()).CreateMapper();
            if (userDTO != null)
                return translateObj.Map<UserDTO, UserEntity>(userDTO);
            
            return null;
        }

        private UserDTO MappingToDTO(UserEntity userEntity)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<UserEntity, UserDTO>()).CreateMapper();

            if (userEntity != null)
                return translateObj.Map<UserEntity, UserDTO>(userEntity);

            return null;
        }
    }
}

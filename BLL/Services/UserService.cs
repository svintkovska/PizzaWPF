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
                var entity = TranslateUserDTOToUserEntity(item);
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
                return TranslateCategoryEntityToCategoryDTO(item);
            }
            return null;
        }

        public IList<UserDTO> GetAll()
        {
            var list = new List<UserDTO>();
            foreach (var user in _userRepository.GetAll())
            {
                list.Add(TranslateCategoryEntityToCategoryDTO(user));
            }
            return list;
        }

        public async Task Update(int id, UserDTO item)
        {
            if (item != null)
            {
                await _userRepository.Update(id, TranslateUserDTOToUserEntity(item));
            }
        }

        private UserEntity TranslateUserDTOToUserEntity(UserDTO userDTO)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<UserDTO, UserEntity>()).CreateMapper();
            if (userDTO != null)
                return new UserEntity()
                {
                    Id = userDTO.Id,
                    FirstName = userDTO.FirstName,
                    LastName = userDTO.LastName,
                    Phone = userDTO.Phone,
                    Email = userDTO.Email,
                    Password = userDTO.Password,
                    DateCreated = userDTO.DateCreated,
                    IsDelete = userDTO.IsDelete
                };
            return null;
        }

        private UserDTO TranslateCategoryEntityToCategoryDTO(UserEntity userEntity)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<UserEntity, UserDTO>()).CreateMapper();

            if (userEntity != null)
                return new UserDTO()
                {
                    Id = userEntity.Id,
                    FirstName = userEntity.FirstName,
                    LastName = userEntity.LastName,
                    Phone = userEntity.Phone,
                    Email = userEntity.Email,
                    Password = userEntity.Password,
                    DateCreated = userEntity.DateCreated,
                    IsDelete = userEntity.IsDelete
                };
            return null;
        }
    }
}

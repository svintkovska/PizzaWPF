using AutoMapper;
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
    public class UserRolesService
    {
        UserRolesRepository _userRolesRepository;
        public UserRolesService()
        {
            _userRolesRepository = new UserRolesRepository();
        }

        public async Task<UserRoleDTO> Create(UserRoleDTO item)
        {
            if (item != null)
            {
                var entity = MappingToEntity(item);
                await _userRolesRepository.Create(entity);
                
                return MappingToDTO(entity);
            }
            return null;
        }

        public IList<UserRoleDTO> GetAll()
        {
            var list = new List<UserRoleDTO>();
            foreach (var user in _userRolesRepository.GetAll())
            {
                list.Add(MappingToDTO(user));
            }
            return list;
        }

        private UserRoleEntity MappingToEntity(UserRoleDTO userRoleDTO)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<UserRoleDTO, UserRoleEntity>()).CreateMapper();
            if (userRoleDTO != null)
                return translateObj.Map<UserRoleDTO, UserRoleEntity>(userRoleDTO);

            return null;
        }

        private UserRoleDTO MappingToDTO(UserRoleEntity userRoleEntity)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<UserRoleEntity, UserRoleDTO>()).CreateMapper();

            if (userRoleEntity != null)
                return translateObj.Map<UserRoleEntity, UserRoleDTO>(userRoleEntity);

            return null;
        }
    }
}

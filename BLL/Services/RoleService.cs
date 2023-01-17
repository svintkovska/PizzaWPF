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
    public class RoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService()
        {
            EFAppContext context = new EFAppContext();
            _roleRepository = new RoleRepository(context);

        }
        public async Task<RoleDTO> Find(int? id)
        {
            if (id != null)
            {
                RoleEntity item = await _roleRepository.GetById((int)id);
                return MappingToDTO(item);
            }
            return null;
        }

        public IList<RoleDTO> GetAll()
        {
            var list = new List<RoleDTO>();
            foreach (var prod in _roleRepository.GetAll())
            {
                list.Add(MappingToDTO(prod));
            }
            return list;
        }

        private RoleDTO MappingToDTO(RoleEntity roleEntity)
        {
            var translateObj = new MapperConfiguration(map => map.CreateMap<RoleEntity, RoleDTO>()).CreateMapper();

            if (roleEntity != null)
                return translateObj.Map<RoleEntity, RoleDTO>(roleEntity);

            return null;
        }
    }
}

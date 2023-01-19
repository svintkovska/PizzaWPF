using DAL.Data;
using DAL.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRolesRepository
    {
        private readonly EFAppContext _dbContext;
        public UserRolesRepository()
        {
            _dbContext = new EFAppContext(); ;
        }

        public async Task Create(UserRoleEntity entity)
        {
            await _dbContext.Set<UserRoleEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

       
        public IQueryable<UserRoleEntity> GetAll()
        {
            return _dbContext.Set<UserRoleEntity>().AsNoTracking();
        }
        public async Task<UserRoleEntity> GetById(int userId, int roleId)
        {
            return await _dbContext.Set<UserRoleEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.UserId == userId && e.RoleId == roleId);
        }

        public async Task Delete(int userId, int roleId)
        {
            var entity = await GetById(userId, roleId);
             _dbContext.Set<UserRoleEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

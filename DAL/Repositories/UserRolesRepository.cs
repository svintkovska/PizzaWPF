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

       
    }
}

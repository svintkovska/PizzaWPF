using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {
        public UserRepository(EFAppContext context) : base(context) { }

        public async Task AddProductToBasket(BasketEntity entity)
        {
            await _dbContext.Set<BasketEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductInBasket(BasketEntity entity)
        {
            _dbContext.Set<BasketEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        public async Task RemoveProductFromBasket(BasketEntity entity)
        {
            _dbContext.Set<BasketEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
   
}

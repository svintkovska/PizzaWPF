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
    public class BasketRepository
    {
        protected readonly EFAppContext _dbContext;
        public BasketRepository(EFAppContext context)
        {
            _dbContext = context;
        }

        public async Task Create(BasketEntity entity)
        {
            await _dbContext.Set<BasketEntity>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int userId, int productId)
        {
            var entity = await GetById(userId, productId);
            _dbContext.Set<BasketEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public IQueryable<BasketEntity> GetAll()
        {
            return _dbContext.Set<BasketEntity>().AsNoTracking();
        }

        public async Task<BasketEntity> GetById(int userId, int productId)
        {
            return await _dbContext.Set<BasketEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.UserId == userId && e.ProductId == productId);
        }

        public async Task Update(BasketEntity entity)
        {
            _dbContext.Set<BasketEntity>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

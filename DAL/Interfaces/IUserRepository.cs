using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUserRepository : IGenericRepository<UserEntity, int>
    {
        Task AddProductToBasket(BasketEntity entity);
        Task UpdateProductInBasket(BasketEntity entity);
        Task RemoveProductFromBasket(BasketEntity entity);

    }
}

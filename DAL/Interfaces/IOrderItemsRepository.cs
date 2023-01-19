using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    public interface IOrderItemsRepository : IGenericRepository<OrderItemEntity, int>
    {
    }
}

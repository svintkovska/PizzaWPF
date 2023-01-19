using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class OrderItemsRepository : GenericRepository<OrderItemEntity>, IOrderItemsRepository
    {
        public OrderItemsRepository(EFAppContext context) : base(context) { }

    }
}

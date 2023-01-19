using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class OrderRepository: GenericRepository<OrderEntity>, IOrderRepository
    {
        public OrderRepository(EFAppContext context) : base(context) { }

    }
}

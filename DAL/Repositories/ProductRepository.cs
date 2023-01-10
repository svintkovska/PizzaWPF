using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class ProductRepository : GenericRepository<ProductEntity>, IProductRepository    
    {
        public ProductRepository(EFAppContext context) : base(context)
        {
        }
    }
}

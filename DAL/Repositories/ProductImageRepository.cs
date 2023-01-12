using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class ProductImageRepository: GenericRepository<ProductImageEntity>, IProductImagesRepository    
    {
        public ProductImageRepository(EFAppContext context) : base(context) { }
    }
}




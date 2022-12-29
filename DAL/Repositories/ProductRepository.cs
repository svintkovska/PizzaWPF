using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class ProductRepository : IRepository<ProductEntity>
    {
        private EFAppContext _context = new EFAppContext();

        public void Create(ProductEntity item)
        {
            if (item != null)
            {
                _context.Products.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(int? id)
        {
            var tempProd = _context.Products.Find(id);
            if (tempProd != null)
            {
                _context.Products.Remove(tempProd);
                _context.SaveChanges();
            }
        }

        public ProductEntity Find(int? id) => _context.Products.Find(id);

        public IEnumerable<ProductEntity> GetAll() => _context.Products;

        public void Update(ProductEntity item)
        {
            var tempProd= _context.Products.Find(item.Id);
            tempProd.Name = item.Name;
            tempProd.Price = item.Price;
            tempProd.DiscountPrice = item.DiscountPrice;
            tempProd.IsOnDiscount = item.IsOnDiscount;
            tempProd.Weight = item.Weight;
            tempProd.Description = item.Description;
            tempProd.CategoryId = item.CategoryId;

           _context.SaveChanges();
        }
    }
}

using DAL.Data;
using DAL.Data.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Repositories
{
    public class CategoryRepository : IRepository<CategoryEntity>
    {
        private EFAppContext _context = new EFAppContext();

        public void Create(CategoryEntity item)
        {
            if (item != null)
            {
                _context.Categories.Add(item);
                _context.SaveChanges();
            }
        }

        public void Delete(int? id)
        {
            var tempCat = _context.Categories.Find(id);
            if (tempCat != null)
            {
                _context.Categories.Remove(tempCat);
                _context.SaveChanges();
            }
        }

        public CategoryEntity Find(int? id) => _context.Categories.Find(id);

        public IEnumerable<CategoryEntity> GetAll() => _context.Categories;

        public void Update(CategoryEntity item)
        {
            var tempCat = _context.Categories.Find(item.Id);
            tempCat.Name = item.Name;
            tempCat.Image = item.Image;
            _context.SaveChanges();
        }
    }
}

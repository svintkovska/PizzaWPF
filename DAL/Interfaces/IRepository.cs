using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Interfaces
{
    interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T Find(int? id);
        void Create(T item);
        void Delete(int? id);
        void Update(T item);
    }
}

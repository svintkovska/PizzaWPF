using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interfaces
{
    interface IService<T> where T : class
    {
        IList<T> GetAll();
        T Find(int? id);
        void Create(T item);
        void Delete(int? id);
        void Update(T item);
    }
}

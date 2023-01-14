using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface IService<T> where T : class
    {
        IList<T> GetAll();
        T Find(int? id);
        Task<int> Create(T item);
        void Delete(int? id);
        void Update(int id, T item);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    interface IService<T> where T : class
    {
        IList<T> GetAll();
        Task<T> Find(int? id);
        Task<int> Create(T item);
        Task Delete(int? id);
        Task Update(int id, T item);
    }
}

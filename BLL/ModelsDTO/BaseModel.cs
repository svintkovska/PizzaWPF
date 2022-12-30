using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ModelsDTO
{
    public interface IModel<T>
    {
        T Id { get; set; }
        bool IsDelete { get; set; }
        DateTime DateCreated { get; set; }
    }

    public abstract class BaseModel<T> : IModel<T>
    {
        public T Id { get; set; }
        public bool IsDelete { get; set; }
        public DateTime DateCreated { get; set; }
    }
}

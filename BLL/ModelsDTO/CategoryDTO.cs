using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ModelsDTO
{
    public class CategoryDTO : BaseModel<int>
    {
        public string Name { get; set; }
        public string Image { get; set; }

        public override string ToString()
        {
            return Name; 
        }
    }
}

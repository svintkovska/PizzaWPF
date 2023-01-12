using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ModelsDTO
{
    public class ProductImageDTO: BaseModel<int>
    {
        public string Name { get; set; }
        public short Priority { get; set; }
        public int ProductId { get; set; }
    }
}

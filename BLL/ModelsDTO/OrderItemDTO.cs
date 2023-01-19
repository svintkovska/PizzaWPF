using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ModelsDTO
{
    public class OrderItemDTO : BaseModel<int>
    {
        public decimal Price { get; set; }
        public short Count { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
    }
}

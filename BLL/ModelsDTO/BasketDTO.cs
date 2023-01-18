using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ModelsDTO
{
    public class BasketDTO
    {
        public short Count { get; set; }
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}

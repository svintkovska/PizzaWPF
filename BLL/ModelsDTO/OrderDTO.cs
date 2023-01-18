using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ModelsDTO
{
    public class OrderDTO : BaseModel<int>
    {
        public int UserId { get; set; }
    }
}

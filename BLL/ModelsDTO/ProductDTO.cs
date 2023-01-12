using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ModelsDTO
{
    public class ProductDTO: BaseModel<int>
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public bool IsOnDiscount { get; set; }
        public int Weight { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}

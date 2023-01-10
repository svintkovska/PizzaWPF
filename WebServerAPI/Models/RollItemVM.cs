using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServerAPI.Models
{
    public class RollItemVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Grams { get; set; }
        public List<string> Description { get; set; }
        public string Img { get; set; }
        public bool Discount { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
    }
}

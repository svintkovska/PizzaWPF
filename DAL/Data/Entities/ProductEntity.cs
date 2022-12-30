using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Data.Entities
{
    [Table("tblProducts")]
    public class ProductEntity: BaseEntity<int>
    {
        [Required, StringLength(255)]
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal DiscountPrice { get; set; }
        public bool IsOnDiscount { get; set; }
        public int Weight { get; set; }
        [ StringLength(4000)]
        public string Description { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public virtual CategoryEntity Category { get; set; }
        public virtual ICollection<ProductImageEntity> Images { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Data.Entities
{
    [Table("tblProductImages")]
    public class ProductImageEntity: BaseEntity<int>
    {
        [Required, StringLength(200)]
        public string Name { get; set; }
        public short Priority { get; set; }
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public virtual ProductEntity Product { get; set; }

    }
}

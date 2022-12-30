using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Data.Entities
{
    [Table("tgblOrders")]
    public class OrderEntity: BaseEntity<int>
    {
        [ForeignKey("OrderStatus")]
        public int StatusId { get; set; }
        public virtual OrderStatusEntity OrderStatus { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual UserEntity User { get; set; }
        public virtual ICollection<OrderItemEntity> OrderItems { get; set; }

    }
}

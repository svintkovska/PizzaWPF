using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DAL.Data.Entities
{
    [Table("tblUsers")]
    public class UserEntity : BaseEntity<int>
    {
        [Required, StringLength(75)]
        public string FirstName { get; set; }
        [Required, StringLength(75)]
        public string LastName { get; set; }
        [Required, StringLength(20)]
        public string Phone { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(255)]
        public string Password { get; set; }

        public virtual ICollection<BasketEntity> Baskets { get; set; }
        public virtual ICollection<OrderEntity> Orders { get; set; }
        public virtual ICollection<UserRoleEntity> UserRoles { get; set; }

    }
}

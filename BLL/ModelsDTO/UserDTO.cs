using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ModelsDTO
{
    public class UserDTO : BaseModel<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}

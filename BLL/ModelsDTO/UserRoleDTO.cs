using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BLL.ModelsDTO
{
    public class UserRoleDTO: INotifyPropertyChanged
    {
        private int _userId;
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (_userId != value)
                {
                    _userId = value;
                    NotifyPropertyCahnged(nameof(UserId));
                }
            }
        }

        private int _roleId;
        public int RoleId
        {
            get
            {
                return _roleId;
            }
            set
            {
                if (_roleId != value)
                {
                    _roleId = value;
                    NotifyPropertyCahnged(nameof(RoleId));
                }
            }
        }

        public virtual UserDTO User { get; set; }
        public virtual RoleDTO Role { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyCahnged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }


    }
}

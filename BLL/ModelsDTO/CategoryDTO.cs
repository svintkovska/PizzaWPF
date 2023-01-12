using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BLL.ModelsDTO
{
    public class CategoryDTO : BaseModel<int>, INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyCahnged(nameof(Name));
                }
            }
        }
        public string _image { get; set; }
        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                if (_image != value)
                {
                    _image = value;
                    NotifyPropertyCahnged(nameof(Image));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyCahnged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
}

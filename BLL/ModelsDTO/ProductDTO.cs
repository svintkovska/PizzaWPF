using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BLL.ModelsDTO
{
    public class ProductDTO: BaseModel<int>, INotifyPropertyChanged
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
        private decimal _price { get; set; }
        public decimal Price
        {
            get
            {
                return _price;
            }
            set
            {
                if (_price != value)
                {
                    _price = value;
                    NotifyPropertyCahnged(nameof(Price));
                }
            }
        }
        private decimal _discountPrice { get; set; }
        public decimal DiscountPrice
        {
            get
            {
                return _discountPrice;
            }
            set
            {
                if (_discountPrice != value)
                {
                    _discountPrice = value;
                    NotifyPropertyCahnged(nameof(DiscountPrice));
                }
            }
        }
        private bool _isOnDiscount { get; set; }
        public bool IsOnDiscount
        {
            get
            {
                return _isOnDiscount;
            }
            set
            {
                if (_isOnDiscount != value)
                {
                    _isOnDiscount = value;
                    NotifyPropertyCahnged(nameof(IsOnDiscount));
                }
            }
        }
        private int _weight { get; set; }
        public int Weight
        {
            get
            {
                return _weight;
            }
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                    NotifyPropertyCahnged(nameof(Weight));
                }
            }
        }
        private string _description { get; set; }
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    NotifyPropertyCahnged(nameof(Description));
                }
            }
        }
        private int _categoryId { get; set; }
        public int CategoryId
        {
            get
            {
                return _categoryId;
            }
            set
            {
                if (_categoryId != value)
                {
                    _categoryId = value;
                    NotifyPropertyCahnged(nameof(CategoryId));
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

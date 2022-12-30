using BLL.ModelsDTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PizzaUI.ViewModels
{
    public class CategoriesVM : INotifyPropertyChanged
    {
        private CategoryService _categoryService;
        private IList<CategoryDTO> _categories;

        public CategoriesVM()
        {
            _categoryService = new CategoryService();
            _categories = _categoryService.GetAll();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public IList<CategoryDTO> GetCategories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                NotifyPropertyChanged("GetCategories");
            }
        }
    }
}

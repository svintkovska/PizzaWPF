using BLL.ModelsDTO;
using BLL.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace PizzaUI.ViewModels
{
    public class CategoriesVM : INotifyPropertyChanged
    {
        private CategoryService _categoryService;
        private ProductService _productService;
        private IList<CategoryDTO> _categories;
        private IList<ProductDTO> _products;

        public CategoriesVM()
        {
            _categoryService = new CategoryService();
            _productService = new ProductService();
            _categories = _categoryService.GetAll();
            _products = _productService.GetAll();
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

        public IList<ProductDTO> GetProducts
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyPropertyChanged("GetProducts");
            }
        }

    }
}

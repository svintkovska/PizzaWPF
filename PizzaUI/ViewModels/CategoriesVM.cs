using BLL.ModelsDTO;
using BLL.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace PizzaUI.ViewModels
{
    public class CategoriesVM : INotifyPropertyChanged
    {
        private CategoryService _categoryService;
        private ProductService _productService;
        private ObservableCollection<CategoryDTO> _categories;
        private ObservableCollection<ProductDTO> _products;

        public CategoriesVM()
        {
            _categoryService = new CategoryService();
            _productService = new ProductService();
            _categories = new ObservableCollection<CategoryDTO>();
            foreach (var item in _categoryService.GetAll())
            {
                if (!item.IsDelete)
                    _categories.Add(item);
            }
            _products = new ObservableCollection<ProductDTO>();
            foreach (var item in _productService.GetAll())
            {
                if(!item.IsDelete)
                  _products.Add(item);
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }

        public ObservableCollection<CategoryDTO> GetCategories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                NotifyPropertyChanged("GetCategories");
            }
        }

        public ObservableCollection<ProductDTO> GetProducts
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

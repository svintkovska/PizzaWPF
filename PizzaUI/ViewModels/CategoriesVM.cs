using BLL.ModelsDTO;
using BLL.Services;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace PizzaUI.ViewModels
{
    public class CategoriesVM : INotifyPropertyChanged
    {
        private CategoryService _categoryService;
        private ProductService _productService;
        private UserRolesService _userRolesService;
        private UserService _userService;
        private RoleService _roleService;
        private ObservableCollection<CategoryDTO> _categories;
        private ObservableCollection<ProductDTO> _products;
        private ObservableCollection<UserDTO> _usersList;
        private ObservableCollection<UserDTO> _adminsList;
        private ObservableCollection<UserRoleDTO> _allUserRoles;
        private ObservableCollection<UserRoleDTO> _allAdminRoles;

        public CategoriesVM()
        {
            _categoryService = new CategoryService();
            _productService = new ProductService();
            _userService = new UserService();
            _userRolesService = new UserRolesService();
            _roleService = new RoleService();

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
            _allUserRoles = new ObservableCollection<UserRoleDTO>();
            _allAdminRoles = new ObservableCollection<UserRoleDTO>();
           
            var roles = _roleService.GetAll();
            int userRoleId = roles.Where(r => r.Name == "User").FirstOrDefault().Id;
            int adminRoleId = roles.Where(r => r.Name == "Admin").FirstOrDefault().Id;
            foreach (var item in _userRolesService.GetAll())
            {
                if(item.RoleId == userRoleId)
                   _allUserRoles.Add(item);
            }
            foreach (var item in _userRolesService.GetAll())
            {
                if (item.RoleId == adminRoleId)
                    _allAdminRoles.Add(item);
            }


            _usersList = new ObservableCollection<UserDTO>();
            foreach (var item in _userService.GetAll())
            {
                foreach (var role in _allUserRoles)
                {
                    if(role.UserId == item.Id && !item.IsDelete)
                        _usersList.Add(item);
                }
            }

            _adminsList = new ObservableCollection<UserDTO>();
            foreach (var item in _userService.GetAll())
            {
                foreach (var role in _allAdminRoles)
                {
                    if (role.UserId == item.Id && !item.IsDelete)
                        _adminsList.Add(item);
                }
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

        public ObservableCollection<UserDTO> GetUsers
        {
            get { return _usersList; }
            set
            {
                _usersList = value;
                NotifyPropertyChanged("GetUsers");
            }
        }
        public ObservableCollection<UserDTO> GetAdmins
        {
            get { return _adminsList; }
            set
            {
                _adminsList = value;
                NotifyPropertyChanged("GetAdmins");
            }
        }

    }
}

using BLL.ModelsDTO;
using BLL.Services;
using DAL.Data;
using DAL.Repositories;
using PizzaUI.Pages;
using PizzaUI.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private UserDTO _loginedUser;
        public UserDTO LoginedUser
        {
            get { return _loginedUser; }
            set { _loginedUser = value; 
                if(_loginedUser != null)
                {
                    userBtn.Visibility = Visibility.Visible;
                    loginBtn.IsEnabled = false;
                    if (CheckIfAdmin())
                        adminBtn.Visibility = Visibility.Visible;
                    else
                        adminBtn.Visibility = Visibility.Hidden;

                    if (CheckIfSuperAdmin())
                    {
                        adminBtn.Visibility = Visibility.Visible;
                        superAdminBtn.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        superAdminBtn.Visibility = Visibility.Hidden;
                    }
                }
                else
                {
                    loginBtn.IsEnabled = true;
                    adminBtn.Visibility = Visibility.Hidden;
                    superAdminBtn.Visibility = Visibility.Hidden;
                }
                OnPropertyChanged(); }
        }
        public MainWindow()
        {
            InitializeComponent();
            DatabaseSeeder.Seed();

            pagesFrame.Content = new CategoriesPage();
        }

        private void logoBtn_Click(object sender, RoutedEventArgs e)
        {
            pagesFrame.Content = new CategoriesPage();
        }

        private void adminBtn_Click(object sender, RoutedEventArgs e)
        {
            pagesFrame.Content = new AdminPage();

        }
        private void superAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            pagesFrame.Content = new SuperAdminPage();
        }
        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            pagesFrame.Content = new LoginPage();
        }

        private void basketBtn_Click(object sender, RoutedEventArgs e)
        {
            pagesFrame.Content = new BasketPage();
        }

        private void userBtn_Click(object sender, RoutedEventArgs e)
        {
            pagesFrame.Content = new UserPage();
        }
        private async void exitBtn_Click(object sender, RoutedEventArgs e)
        {
            _loginedUser = null;
            loginBtn.IsEnabled = true;
            adminBtn.Visibility = Visibility.Hidden;
            superAdminBtn.Visibility = Visibility.Hidden;
            userBtn.Visibility = Visibility.Hidden;
            pagesFrame.Content = new CategoriesPage();

            OrderService _orderService = new OrderService();
             await _orderService.ClearBasket();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       
        private bool CheckIfAdmin()
        {
            int id = _loginedUser.Id;
            RoleService roleService = new RoleService();
            int roleId = roleService.GetAll().Where(r => r.Name == "Admin").FirstOrDefault().Id;

            UserRolesService userRolesService = new UserRolesService();
            var userRoles = userRolesService.GetAll();

            foreach (var r in userRoles)
            {
                if (r.UserId == id && r.RoleId == roleId)
                    return true;
            }

            return false;
        }


        private bool CheckIfSuperAdmin()
        {
            int id = _loginedUser.Id;
            RoleService roleService = new RoleService();
            int roleId = roleService.GetAll().Where(r => r.Name == "Super Admin").FirstOrDefault().Id;

            UserRolesService userRolesService = new UserRolesService();
            var userRoles = userRolesService.GetAll();

            foreach (var r in userRoles)
            {
                if (r.UserId == id && r.RoleId == roleId)
                    return true;
            }

            return false;
        }
       
    }
}

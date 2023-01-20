using BLL.ModelsDTO;
using BLL.Services;
using PizzaUI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaUI.Pages
{
    /// <summary>
    /// Interaction logic for SuperAdminPage.xaml
    /// </summary>
    public partial class SuperAdminPage : Page
    {
        UserRolesService _userRolesService = new UserRolesService();
        RoleService _roleService = new RoleService();
        public SuperAdminPage()
        {
            InitializeComponent();
        }

        private async void setAsAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            var roles = _roleService.GetAll();
            int adminRoleId = roles.Where(r => r.Name == "Admin").FirstOrDefault().Id;
            var user = allUsersCombobox.SelectedItem as UserDTO;
            var userRole = new UserRoleDTO()
            {
                RoleId = adminRoleId,
                UserId = user.Id
            };
            await _userRolesService.Create(userRole);

            var cm = DataContext as CategoriesVM;
            cm.GetUsers.Remove(user);
            cm.GetAdmins.Add(user);
            allUsersCombobox.SelectedIndex = -1;
        }

        private async void removeAsAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            var roles = _roleService.GetAll();
            int adminRoleId = roles.Where(r => r.Name == "Admin").FirstOrDefault().Id;
            var user = allAdminsCombobox.SelectedItem as UserDTO;          
            await _userRolesService.Delete(user.Id, adminRoleId);

            var cm = DataContext as CategoriesVM;
            cm.GetAdmins.Remove(user);
            cm.GetUsers.Add(user);
            allAdminsCombobox.SelectedIndex = -1;
        }
    }
}

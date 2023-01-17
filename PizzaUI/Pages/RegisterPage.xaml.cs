using BLL.ModelsDTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        UserService userService = new UserService();
        UserRolesService userRolesService = new UserRolesService();
        RoleService roleService = new RoleService();
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void register_btn_Click(object sender, RoutedEventArgs e)
        {
            string hashPassword;
            string StrPassword = password_user_reg.Password;
            using (MD5 md5Hash = MD5.Create())
            {
                hashPassword = GetMd5Hash(md5Hash, StrPassword);
            }

            var user = new UserDTO
            {
                FirstName = first_name_user_reg.Text,
                LastName = last_name_user_reg.Text,
                Phone = phone_user_reg.Text,
                Email = email_user_reg.Text,
                Password = hashPassword
            };

            int userId = await userService.Create(user);
            int roleId = roleService.GetAll().Where(r => r.Name == "User").FirstOrDefault().Id;
            var userRole = new UserRoleDTO()
            {
                UserId = userId,
                RoleId = roleId
            };
            await userRolesService.Create(userRole);

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.pagesFrame.Navigate(new LoginPage());
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

        private readonly Regex validateEmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private readonly Regex validatePhoneNumberRegex = new Regex("^[+][3][8][0][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]$");


        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            if (String.IsNullOrEmpty(first_name_user_reg.Text))
                register_btn.IsEnabled = false;
            else if (String.IsNullOrEmpty(last_name_user_reg.Text))
                register_btn.IsEnabled = false;
            else if (!validatePhoneNumberRegex.IsMatch(phone_user_reg.Text))
                register_btn.IsEnabled = false;
            else if (!validateEmailRegex.IsMatch(email_user_reg.Text))
                register_btn.IsEnabled = false;
            else if (String.IsNullOrEmpty(password_user_reg.Password))
                register_btn.IsEnabled = false;
            else if (String.IsNullOrEmpty(conf_password_user_reg.Password))
                register_btn.IsEnabled = false;
            else if(password_user_reg.Password != conf_password_user_reg.Password)
                register_btn.IsEnabled = false;
            else
                register_btn.IsEnabled = true;
        }
    }
}

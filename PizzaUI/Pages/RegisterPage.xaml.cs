using BLL.ModelsDTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
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
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {
        UserService userService = new UserService();

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

            await userService.Create(user);

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
    }
}

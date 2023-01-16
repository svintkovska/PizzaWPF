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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        UserService userService = new UserService();

        public LoginPage()
        {
            InitializeComponent();
        }

        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            string password = "";
            using (MD5 md5Hash = MD5.Create())
            {
                password = GetMd5Hash(md5Hash, password_user.Password);
            }


            UserDTO user = null;

            var users = userService.GetAll();
            foreach (var u in users)
            {
                if (u.Password == password && u.Email == email_user.Text)
                    user = u;
            }

            if (user != null)
            {
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.pagesFrame.Navigate(new CategoriesPage());
            }
            else
            {
                MessageBox.Show("Error email or password");
            }
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

        private void registerBtn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.pagesFrame.Navigate(new RegisterPage());
        }
    }
}

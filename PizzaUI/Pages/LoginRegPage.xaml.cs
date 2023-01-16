using BLL.ModelsDTO;
using BLL.Services;
using DAL.Data;
using DAL.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for LoginRegPage.xaml
    /// </summary>
    public partial class LoginRegPage : Page
    {
        UserService userService = new UserService();
        
        public LoginRegPage()
        {
            InitializeComponent();
        }
        private async void login_btn_Click(object sender, RoutedEventArgs e)
        {
            string password = "";
            using (MD5 md5Hash = MD5.Create())
            {
                password = GetMd5Hash(md5Hash, password_user.Text);
            }
            //EFAppContext context = new EFAppContext();
            //var user = await context.Users.FirstOrDefault(x => x.Email == email_user.Text && x.Password == password);

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
                mainWindow.pagesFrame.Navigate(new CategoriesPage() );
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

        private async void register_btn_Click(object sender, RoutedEventArgs e)
        {
            string hashPassword;
            string StrPassword = password_user_reg.Text;
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
        }
    }
}

using DAL.Data;
using DAL.Data.Entities;
using PizzaUI.Pages;
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
using System.Windows.Shapes;

namespace PizzaUI
{
    /// <summary>
    /// Interaction logic for Login_Reg_Window.xaml
    /// </summary>
    public partial class Login_Reg_Window : Window
    {
        EFAppContext context = new EFAppContext();
        public Login_Reg_Window()
        {
            InitializeComponent();
        }
        private void login_btn_Click(object sender, RoutedEventArgs e)
        {
            string password = "";
            using (MD5 md5Hash = MD5.Create())
            {
                password = GetMd5Hash(md5Hash, password_user.Text);
            }
            var user = context.Users.FirstOrDefault(x => x.Email == email_user.Text && x.Password == password);

            if (user != null)
            {
                //CategoriesPage categoriesPage = new CategoriesPage();
                //this.Content = categoriesPage;
                MainWindow main = new MainWindow();
                main.Show();
                this.Close();
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

        private void register_btn_Click(object sender, RoutedEventArgs e)
        {
            string hashPassword;
            string StrPassword = password_user_reg.Text;
            using (MD5 md5Hash = MD5.Create())
            {
                hashPassword = GetMd5Hash(md5Hash, StrPassword);
            }

            var user = new UserEntity
            {
                FirstName = first_name_user_reg.Text,
                LastName = last_name_user_reg.Text,
                Phone = phone_user_reg.Text,
                Email = email_user_reg.Text,
                Password = hashPassword
            };

            context.Users.Add(user);
            context.SaveChanges();
            //CategoriesPage categoriesPage = new CategoriesPage();
            //this.Content = categoriesPage;
            MainWindow main = new MainWindow();
            main.Show();
            this.Close();
        }
    }
}

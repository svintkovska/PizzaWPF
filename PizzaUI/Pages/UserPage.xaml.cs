using BLL.ModelsDTO;
using BLL.Services;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        UserDTO user = new UserDTO();
        UserService _userService = new UserService();
        public UserPage()
        {
            InitializeComponent();
            user = (App.Current.MainWindow as MainWindow).LoginedUser;
            if (user != null)
            {
                userName_tb.Text = user.FirstName;
                userLastName_tb.Text = user.LastName;
                userEmail_tb.Text = user.Email;
                userPhone_tb.Text = user.Phone;
            }
        }
        private readonly Regex validateEmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private readonly Regex validatePhoneNumberRegex = new Regex("^[+][3][8][0][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]$");

        private async void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(userName_tb.Text))
                user.FirstName = userName_tb.Text;
            if (!String.IsNullOrEmpty(userLastName_tb.Text))
                user.LastName = userLastName_tb.Text;
            if (!String.IsNullOrEmpty(userEmail_tb.Text) && validateEmailRegex.IsMatch(userEmail_tb.Text))
                user.Email = userEmail_tb.Text;
            if (!String.IsNullOrEmpty(userPhone_tb.Text) && validatePhoneNumberRegex.IsMatch(userPhone_tb.Text))
                user.Phone = userPhone_tb.Text;

            await _userService.Update(user.Id, user);

        }

        private async void changePassBtn_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = " ";
            using (MD5 md5Hash = MD5.Create())
            {
                newPassword = GetMd5Hash(md5Hash, userNewPass_tb.Password);
            }

            user.Password = newPassword;
            await _userService.Update(user.Id, user);


            userOldPass_tb.Password = "";
            userNewPass_tb.Password = "";
            userConfirmPass_tb.Password = "";

        }
        private void passwordGrid_MouseMove(object sender, MouseEventArgs e)
        {
            ValidateNewPassword();
        }
        private void ValidateNewPassword()
        {

            string oldPassword = " ";
            string userPassword = user.Password;
            using (MD5 md5Hash = MD5.Create())
            {
                oldPassword = GetMd5Hash(md5Hash, userOldPass_tb.Password);
            }

            if(oldPassword != userPassword)
                changePassBtn.IsEnabled = false;
            else if (String.IsNullOrEmpty(userNewPass_tb.Password))
                changePassBtn.IsEnabled = false;
            else if(String.IsNullOrEmpty(userConfirmPass_tb.Password))
                changePassBtn.IsEnabled = false;
            else if(userNewPass_tb.Password != userConfirmPass_tb.Password)
                changePassBtn.IsEnabled = false;
            else
                changePassBtn.IsEnabled = true;
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

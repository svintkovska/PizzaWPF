using BLL.ModelsDTO;
using BLL.Services;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for MakeOrderPage.xaml
    /// </summary>
    public partial class MakeOrderPage : Page
    {
        UserDTO _user = (App.Current.MainWindow as MainWindow).LoginedUser;
        OrderService _orderService = new OrderService();
        public MakeOrderPage()
        {
            InitializeComponent();
            if (_user != null)
                userPhoneTB.Text = _user.Phone;
        }

        private async  void confirmOrderBtn_Click(object sender, RoutedEventArgs e)
        {
            int orderId = await _orderService.CreateOrder(_user.Id);
            await  _orderService.CreateOrderItems(_user.Id, orderId);


            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.pagesFrame.Navigate(new OrderReadyPage());
        }
        private readonly Regex validatePhoneNumberRegex = new Regex("^[+][3][8][0][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]$");

        private void ValidateOrderDetails()
        {
            if (!validatePhoneNumberRegex.IsMatch(userPhoneTB.Text))
                confirmOrderBtn.IsEnabled = false;
            else if (String.IsNullOrEmpty(streetTB.Text))
                confirmOrderBtn.IsEnabled = false;
            else if (String.IsNullOrEmpty(houseTB.Text))
                confirmOrderBtn.IsEnabled = false;
            else if (cityComboBox.SelectedItem == null)
                confirmOrderBtn.IsEnabled = false;
            else
                confirmOrderBtn.IsEnabled = true;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            ValidateOrderDetails();
        }
    }
}

using BLL.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Interaction logic for OrderReadyPage.xaml
    /// </summary>
    public partial class OrderReadyPage : Page
    {
        OrderService _orderService = new OrderService();

        public OrderReadyPage()
        {
            InitializeComponent();
            _orderService.ClearBasket();
        }

        private void backtoMenuBnt_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.pagesFrame.Navigate(new CategoriesPage());
        }

        private void instaBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.instagram.com/lapizza_ternopil/",
                UseShellExecute = true
            });
        }

        private void facebookBtn_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = "https://www.facebook.com/lapizza.ternopil",
                UseShellExecute = true
            });
        }
    }
}

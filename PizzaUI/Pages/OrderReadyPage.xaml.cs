using BLL.Services;
using System;
using System.Collections.Generic;
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

       
    }
}

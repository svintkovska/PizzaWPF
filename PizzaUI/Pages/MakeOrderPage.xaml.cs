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
    /// Interaction logic for MakeOrderPage.xaml
    /// </summary>
    public partial class MakeOrderPage : Page
    {
        public MakeOrderPage()
        {
            InitializeComponent();
            var user = (App.Current.MainWindow as MainWindow).LoginedUser;
            if (user != null)
                userPhoneTB.Text = user.Phone;
        }
    }
}

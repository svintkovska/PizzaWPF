using DAL.Data;
using PizzaUI.Pages;
using PizzaUI.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PizzaUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DatabaseSeeder.Seed();

            pagesFrame.Content = new CategoriesPage();
        }

        private void logoBtn_Click(object sender, RoutedEventArgs e)
        {
            pagesFrame.Content = new CategoriesPage();
        }

        private void adminBtn_Click(object sender, RoutedEventArgs e)
        {
            pagesFrame.Content = new AdminPage();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            pagesFrame.Content = new LoginRegPage();
        }

        private void basketBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void exitBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

using BLL.ModelsDTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Interaction logic for SeparateProductPage.xaml
    /// </summary>
    public partial class SeparateProductPage : Page
    {
        UserDTO _user = (App.Current.MainWindow as MainWindow).LoginedUser;
        ProductDTO _product = new ProductDTO();
        OrderService _orderService = new OrderService();
        public SeparateProductPage()
        {
            InitializeComponent();
        }

       public SeparateProductPage(Dictionary<ProductDTO, List<ProductImageDTO>> product): this()
       {
            ProductDTO current = product.Keys.First();
            _product = current;
            List<ProductImageDTO> images = product[current].OrderBy(i => i.Priority).ToList();

            if (current.Name != null)
            {
                NamePr.Text = current.Name.ToString();
            }
            else
            {
                NamePr.Text = string.Empty;
            }

            if (current.Description != null)
            {
                DescriptionPr.Text = current.Description.ToString();
            }
            else
            {
                DescriptionPr.Text = string.Empty;
            }

            if (current.Price != null )
            {
                PricePr.Text = current.Price.ToString() + " UAH";
            }
            else
            {
                PricePr.Text = string.Empty;
            }

            if (current.Weight != null)
            {
                WeightPr.Text = "Weight: " + current.Weight.ToString();
            }
            else
            {
                WeightPr.Text = string.Empty;
            }

            foreach (ProductImageDTO image in images) 
            {
              Album.Items.Add(new Image() { Source = new BitmapImage(new Uri(image.Name)), Stretch = Stretch.UniformToFill });
            }
            var listBoxItem =(ListBoxItem)Album.ItemContainerGenerator.ContainerFromIndex(0);

            Album.SelectedIndex = 0;
            
        }


        

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            
            int c = Convert.ToInt32(Counter.Text.ToString());
            if(c > 1) 
            {
                c--;
                Counter.IsReadOnly = false;
                Counter.Text = c.ToString();
                Counter.IsReadOnly = true;
            }
            
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            int c = Convert.ToInt32(Counter.Text.ToString());
            if (c < 1000)
            {
                c++;
                Counter.IsReadOnly = false;
                Counter.Text = c.ToString();
                Counter.IsReadOnly = true;
            }
        }

        private async void AddBasket_Click(object sender, RoutedEventArgs e)
        {
            if(_user == null)
            {
                Counter.Text = "0";
                var mainWindow = (MainWindow)Application.Current.MainWindow;
                mainWindow.pagesFrame.Navigate(new LoginPage());
            }
            else
            {
                short count = short.Parse(Counter.Text);
                BasketDTO basketItem = new BasketDTO()
                {
                    UserId = _user.Id,
                    ProductId = _product.Id,
                    Count = count
                };
                await _orderService.AddToBasket(basketItem);
                Counter.Text = "0";
            }      
        }


        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            ListBoxItem Item = (sender as ListBoxItem);
            Image im = Item.Content as Image;
            MainImage.Source = im.Source;
        }

        private void btnPreviousPage_Click(object sender, RoutedEventArgs e)
        {
            var navigationService = (App.Current.MainWindow as MainWindow).pagesFrame.NavigationService;
            navigationService.GoBack();
        }

        private void btnMainPage_Click(object sender, RoutedEventArgs e)
        {
            var navigationService = (App.Current.MainWindow as MainWindow).pagesFrame.NavigationService;
            navigationService.GoBack();
            navigationService.GoBack();
        }
    }
}

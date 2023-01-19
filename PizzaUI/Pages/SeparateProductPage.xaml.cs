using BLL.ModelsDTO;
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
        public SeparateProductPage()
        {
            InitializeComponent();
        }

       public SeparateProductPage(Dictionary<ProductDTO, List<ProductImageDTO>> product): this()
       {
            ProductDTO current = product.Keys.First();
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

            if (current.Price != null)
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

        private void AddBasket_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListBoxItem_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ListBoxItem_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {
            ListBoxItem Item = (sender as ListBoxItem);
            Image im = Item.Content as Image;
            MainImage.Source = im.Source;
        }
    }
}

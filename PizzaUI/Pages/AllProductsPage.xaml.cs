using BLL.ModelsDTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Net.Mime.MediaTypeNames;
using System.Linq;
using System.Windows.Shell;

namespace PizzaUI.Pages
{
    /// <summary>
    /// Interaction logic for AllProducts.xaml
    /// </summary>
    public partial class AllProducts : Page
    {
        ProductService productService;
        CategoryService categoryService;
        ProductImageService productImageService;
        IList<ProductDTO> productDTOs;
        IList<CategoryDTO> categoryDTOs;
        IList<ProductImageDTO> productImageDTOs;
        string selectedCategory = null;

        List<Grid> checkedItems = new List<Grid>();

        Dictionary<CategoryDTO, Dictionary<ProductDTO, List<ProductImageDTO>>> data = new Dictionary<CategoryDTO, Dictionary<ProductDTO, List<ProductImageDTO>>>();
        public AllProducts()
        {
            InitializeComponent();
            productService = new ProductService();  
            categoryService = new CategoryService();
            productImageService = new ProductImageService();
            
        }

        public AllProducts(string categoryName) : this()
        {
            selectedCategory = categoryName;
            setData();
            FillData();
            loadItems();
        }

        private void setData()
        {
            productDTOs = productService.GetAll();
            categoryDTOs = categoryService.GetAll();
            productImageDTOs = productImageService.GetAll();
        }



        private void loadItems()
        {
            foreach (CategoryDTO category in data.Keys)
            {
                if (category.Name == selectedCategory)
                {
                    foreach (ProductDTO product in data[category].Keys)
                    {
                        int i = 0;
                        Grid item = CreateItem(data[category][product], product.Price, product.Name, product.Weight);
                        ListBixItems.Items.Add(item);
                    }
                    break;
                }
            }
        }


        private void FillData()
        {
            foreach(CategoryDTO category in categoryDTOs)
            {
                data.Add(category, new Dictionary<ProductDTO, List<ProductImageDTO>>());
            }
            
            foreach (ProductDTO productDTO in productDTOs) 
            {
                if(productDTO.IsDelete == false)
                {
                    List<ProductImageDTO> tempPhotos =  new List<ProductImageDTO>();
                    foreach (ProductImageDTO productImageDTO in productImageDTOs)
                    {
                        if(productImageDTO.ProductId == productDTO.Id && productImageDTO.IsDelete == false)
                        {
                            tempPhotos.Add(productImageDTO);
                        }
                    }

                    foreach(var key in data.Keys)
                    {
                        if(key.Id == productDTO.CategoryId)
                        {
                            data[key].Add(productDTO, tempPhotos);
                        }
                    }

                }
                       
            }
        }


        private Grid CreateItem(List<ProductImageDTO> img, decimal price, string name, float weight)
        {

            List<ProductImageDTO> images = img.OrderBy(i => i.Priority).ToList();


            Grid TempItem = new Grid();
            System.Windows.Controls.Image Im = new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(images[0].Name))};
            TextBlock Price = new TextBlock() { Text = "Price: "+ price.ToString() };
            TextBlock Name = new TextBlock() { Text = name };
            TextBlock Weight = new TextBlock() { Text = "Weight: " + weight.ToString() };
            
            Grid counter = Counter();
            Grid.SetColumn(counter, 1);
            Grid.SetRow(counter, 0);
            counter.HorizontalAlignment = HorizontalAlignment.Right;
            counter.VerticalAlignment = VerticalAlignment.Top;

            Im.Stretch = Stretch.UniformToFill;
            Im.Height = 20;
            Im.Width = 20;
            Name.FontSize = 20;
            Name.HorizontalAlignment= HorizontalAlignment.Center;
            Weight.FontSize = 14;
            Weight.HorizontalAlignment = HorizontalAlignment.Center;
            Price.FontSize = 14;
            Price.HorizontalAlignment = HorizontalAlignment.Center;

            TempItem.ColumnDefinitions.Add(new ColumnDefinition());
            TempItem.ColumnDefinitions.Add(new ColumnDefinition());
            TempItem.RowDefinitions.Add(new RowDefinition());
            TempItem.RowDefinitions.Add(new RowDefinition());
            TempItem.RowDefinitions.Add(new RowDefinition());

            Grid.SetRowSpan(Im, 2);
            Grid.SetColumn(Im, 0);
            Grid.SetRow(Im, 0);

            Grid.SetColumn(Name, 1);
            Grid.SetRow(Name, 0);

            Grid.SetColumn(Weight, 1);
            Grid.SetRow(Weight, 1);

            Grid.SetColumn(Price, 1);
            Grid.SetRow(Price, 2);


            Grid album = CreateAlbom(images);




            TempItem.Height = 40;
            TempItem.Children.Add(Im);
            TempItem.Children.Add(Name);
            TempItem.Children.Add(Weight);
            TempItem.Children.Add(Price);
            TempItem.Children.Add(counter);
            TempItem.Children.Add(album);

            return TempItem;
        }

        private Grid CreateAlbom(List<ProductImageDTO> images)
        {
            Grid album = new Grid();
            album.VerticalAlignment = VerticalAlignment.Center;
            album.HorizontalAlignment = HorizontalAlignment.Left;
            Grid.SetColumn(album, 0);
            Grid.SetRow(album, 2);
            for (int i = 0; i < images.Count; i++)
            {
                album.ColumnDefinitions.Add(new ColumnDefinition());
            }

            foreach(ProductImageDTO currentImage in images)
            {
                int i = 0;
                System.Windows.Controls.Image image = new System.Windows.Controls.Image();
                image.Source = new BitmapImage(new Uri(currentImage.Name));
                image.Width = 10;
                image.Height = 10;
                image.VerticalAlignment = VerticalAlignment.Center;
                image.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetColumn(image, i);
                album.Children.Add(image);
                i++;
            }

            return album;
        }




        private Grid Counter()
        {

            //< Grid  Height = "26" Margin = "152,63,11,0" >
            //    < TextBox x: Name = "txtNum" x: FieldModifier = "private" FontSize = "13" Text = "0" TextChanged = "txtNum_TextChanged" Margin = "0,3,14,3" />
            //    < Button x: Name = "cmdUp" x: FieldModifier = "private" FontSize = "12" Padding = "0,-4,0,0" Content = "▲" Width = "12" Click = "cmdUp_Click" Margin = "33,1,1,14" />
            //    < Button x: Name = "cmdDown" x: FieldModifier = "private" FontSize = "12" Padding = "0,-4,0,0" Content = "▼" Width = "12" Click = "cmdDown_Click" Margin = "33,14,1,1" />
            //</ Grid >

            Grid TempItem = new Grid();
            TempItem.Height = 26;
            TempItem.IsEnabled = false;
            TempItem.HorizontalAlignment = HorizontalAlignment.Right;

            TextBox num = new TextBox();
            num.Text = "0";
            num.Margin = new Thickness(3, 2, 13, 3);
            num.FontSize = 13;
            //num.IsReadOnly = true;


            //num.GotFocus += textBox1_GotFocus;

            Button Up= new Button();
            Up.FontSize = 12;
            Up.Padding = new Thickness(0, -4, 0, 0);
            Up.Content = "▲";
            Up.Width = 12;
            Up.Margin = new Thickness(33, 1, 1, 14);
            Up.Click += cmdUp_Click;


            Button Dn= new Button();
            Dn.FontSize = 12;
            Dn.Padding = new Thickness(0, -4, 0, 0);
            Dn.Content = "▼";
            Dn.Width = 12;
            Dn.Margin = new Thickness(33, 14, 1, 1);
            Dn.Click += cmdDown_Click;

            TempItem.Children.Add(num);
            TempItem.Children.Add(Up);
            TempItem.Children.Add(Dn);


            Grid outer = new Grid();
            outer.Width = 60;
            Grid.SetColumn(outer, 1);
            Grid.SetRow(outer, 0);
            outer.HorizontalAlignment = HorizontalAlignment.Right;
            outer.VerticalAlignment = VerticalAlignment.Top;

            CheckBox choose = new CheckBox();
            choose.HorizontalAlignment = HorizontalAlignment.Left;
            choose.Checked += Choose_Checked;
            choose.Unchecked += Choose_Unchecked; 




            outer.Children.Add(choose);
            outer.Children.Add(TempItem);
            


            return outer;
        }

        private void Choose_Unchecked(object sender, RoutedEventArgs e)
        {

            ((((sender as CheckBox).Parent as Grid).Children[1] as Grid).Children[0] as TextBox).Text = "0";
            (((sender as CheckBox).Parent as Grid).Children[1] as Grid).IsEnabled = false;
            checkedItems.Remove((sender as CheckBox).Parent as Grid);
        }

        private void Choose_Checked(object sender, RoutedEventArgs e)
        {
            checkedItems.Add((sender as CheckBox).Parent as Grid);
             (((sender as CheckBox).Parent as Grid).Children[1] as Grid).IsEnabled = true;
        }


        private void cmdUp_Click(object sender, RoutedEventArgs e)
        {
            TextBlock counter = ((sender as Button).Parent as Grid).Children[0] as TextBlock;
            counter.Text = (Convert.ToInt32(counter.Text.ToString()) + 1).ToString();
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            TextBlock counter = ((sender as Button).Parent as Grid).Children[0] as TextBlock;

            if (Convert.ToInt32(counter.Text.ToString()) == 0)
                return;
            counter.Text = (Convert.ToInt32(counter.Text.ToString()) - 1).ToString();
           
        }


    }
}

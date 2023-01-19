using BLL.ModelsDTO;
using BLL.Services;
using DAL.Data;
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
    /// Interaction logic for BasketPage.xaml
    /// </summary>
    public partial class BasketPage : Page
    {
        ProductService productService;
        CategoryService categoryService;
        ProductImageService productImageService;
        IList<ProductDTO> productDTOs;
        IList<CategoryDTO> categoryDTOs;
        IList<ProductImageDTO> productImageDTOs;
        EFAppContext context = new EFAppContext();
        string selectedCategory = null;

        //List<Grid> checkedItems = new List<Grid>();

        Dictionary<CategoryDTO, Dictionary<ProductDTO, List<ProductImageDTO>>> data = new Dictionary<CategoryDTO, Dictionary<ProductDTO, List<ProductImageDTO>>>();
        public BasketPage()
        {
            InitializeComponent();
            productService = new ProductService();
            categoryService = new CategoryService();
            productImageService = new ProductImageService();
        }

        public BasketPage(CategoryDTO category) : this()
        {
            selectedCategory = category.Name;
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
                        //int i = 1;
                        Grid item = CreateItem(data[category][product], product.Price, product.Name, product.Id);
                        //if(i==1)
                        //{
                        //    item.Margin = new Thickness(0, 0, 5, 0);
                        //}
                        //else if(i == 2)
                        //{
                        //    item.Margin = new Thickness(5, 0, 0, 0);
                        //}
                        ListBoxBasket.Items.Add(item);


                        //i += (i == 1) ? -1 : 1;
                    }
                    break;
                }
            }
        }


        private void FillData()
        {
            foreach (CategoryDTO category in categoryDTOs)
            {
                data.Add(category, new Dictionary<ProductDTO, List<ProductImageDTO>>());
            }

            foreach (ProductDTO productDTO in productDTOs)
            {
                if (productDTO.IsDelete == false)
                {
                    List<ProductImageDTO> tempPhotos = new List<ProductImageDTO>();
                    foreach (ProductImageDTO productImageDTO in productImageDTOs)
                    {
                        if (productImageDTO.ProductId == productDTO.Id && productImageDTO.IsDelete == false)
                        {
                            tempPhotos.Add(productImageDTO);
                        }
                    }

                    foreach (var key in data.Keys)
                    {
                        if (key.Id == productDTO.CategoryId)
                        {
                            data[key].Add(productDTO, tempPhotos);
                        }
                    }

                }

            }
        }

        private Grid CreateItem(List<ProductImageDTO> img, decimal price, string name, int _id)
        {

            List<ProductImageDTO> images = img.OrderBy(i => i.Priority).ToList();

            TextBlock Id = new TextBlock() { Text = _id.ToString() };
            Id.Opacity = 0;
            Id.IsEnabled = false;
            Grid TempItem = new Grid();
            System.Windows.Controls.Image Im = new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(images[0].Name)) };
            TextBlock Price = new TextBlock() { Text = price.ToString() + "UAH" };
            TextBlock Name = new TextBlock() { Text = name.ToUpper() };
            Button DelBt = new Button() { Content = "Delete" };

            Im.Stretch = Stretch.UniformToFill;
            Im.Height = 150;
            Im.Width = 150;
            Im.HorizontalAlignment = HorizontalAlignment.Center;
            Name.FontSize = 14;
            Name.HorizontalAlignment = HorizontalAlignment.Center;
            Name.VerticalAlignment = VerticalAlignment.Center;
            Name.FontWeight = FontWeights.Bold;
            Name.Foreground = Brushes.White;
            Name.TextWrapping = TextWrapping.WrapWithOverflow;
            Name.TextAlignment = TextAlignment.Center;

            Price.FontSize = 14;
            Price.HorizontalAlignment = HorizontalAlignment.Left;
            Price.VerticalAlignment = VerticalAlignment.Center;
            Price.Foreground = Brushes.White;
            Price.Margin = new Thickness(4, 0, 0, 0);

            DelBt.Background = Brushes.Orange;
            DelBt.Width = 50;
            DelBt.Height = 30;
            DelBt.HorizontalAlignment = HorizontalAlignment.Center;
            DelBt.FontWeight = FontWeights.Bold;
            DelBt.FontSize = 15;


            TempItem.ColumnDefinitions.Add(new ColumnDefinition());
            TempItem.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(60) });
            TempItem.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(150) });
            TempItem.RowDefinitions.Add(new RowDefinition());
            TempItem.RowDefinitions.Add(new RowDefinition());



            Grid.SetRowSpan(Im, 1);
            Grid.SetColumnSpan(Im, 3);
            Grid.SetColumn(Im, 0);
            Grid.SetRow(Im, 0);

            Grid.SetColumn(Name, 0);
            Grid.SetColumnSpan(Name, 2);
            Grid.SetRow(Name, 1);

            Grid.SetColumn(Price, 0);
            Grid.SetRow(Price, 2);

            Grid.SetColumn(DelBt, 1);
            Grid.SetRow(DelBt, 2);

            TempItem.Height = 40;
            TempItem.Children.Add(Im);
            TempItem.Children.Add(Name);
            TempItem.Children.Add(Price);
            TempItem.Children.Add(DelBt);
            TempItem.Children.Add(Id);
            TempItem.Width = 170;
            TempItem.Height = 230;
            TempItem.Background = (Brush)(new BrushConverter().ConvertFrom("#FF202020"));

            return TempItem;
        }

        private void order_btn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.pagesFrame.Navigate(new MakeOrderPage());
        }
    }
}


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

        public AllProducts(CategoryDTO category) : this()
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
                        int i = 1;
                        Grid item = CreateItem(data[category][product], product.Price, product.Name, product.Weight, product.Id);
                        //if(i==1)
                        //{
                        //    item.Margin = new Thickness(0, 0, 5, 0);
                        //}
                        //else if(i == 2)
                        //{
                        //    item.Margin = new Thickness(5, 0, 0, 0);
                        //}
                        ListBixItems.Items.Add(item);


                        i += (i == 1) ? -1 : 1;
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


        private Grid CreateItem(List<ProductImageDTO> img, decimal price, string name, float weight, int _id)
        {

            List<ProductImageDTO> images = img.OrderBy(i => i.Priority).ToList();

            TextBlock Id = new TextBlock() { Text = _id.ToString() };
            Id.Opacity = 0;
            Id.IsEnabled = false;
            Grid TempItem = new Grid();
            System.Windows.Controls.Image Im = new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(images[0].Name)) };
            TextBlock Price = new TextBlock() { Text = price.ToString() + "UAH." };
            TextBlock Name = new TextBlock() { Text = name.ToUpper() };
            TextBlock Weight = new TextBlock() { Text = weight.ToString() + "g." };
            Button AddBt = new Button() { Content = "ADD" };

            //Grid counter = Counter();
            //Grid.SetColumn(counter, 1);
            //Grid.SetRow(counter, 0);
            //counter.HorizontalAlignment = HorizontalAlignment.Right;
            //counter.VerticalAlignment = VerticalAlignment.Top;

            Im.Stretch = Stretch.UniformToFill;
            Im.Height = 150;
            Im.Width = 150;
            Im.HorizontalAlignment = HorizontalAlignment.Center;
            //Im.Margin = new Thickness(5, 0, 0, 0);
            Name.FontSize = 14;
            Name.HorizontalAlignment = HorizontalAlignment.Center;
            Name.VerticalAlignment = VerticalAlignment.Center;
            Name.FontWeight = FontWeights.Bold;
            Name.Foreground = Brushes.White;
            Name.TextWrapping = TextWrapping.WrapWithOverflow;
            Name.TextAlignment = TextAlignment.Center;

            Weight.FontSize = 14;
            Weight.HorizontalAlignment = HorizontalAlignment.Right;
            Weight.VerticalAlignment = VerticalAlignment.Center;
            //Weight.FontWeight = FontWeights.Bold;
            Weight.Foreground = Brushes.White;

            Price.FontSize = 14;
            Price.HorizontalAlignment = HorizontalAlignment.Left;
            Price.VerticalAlignment = VerticalAlignment.Center;
            //Price.FontWeight = FontWeights.Bold;
            Price.Foreground = Brushes.White;
            Price.Margin = new Thickness(4, 0, 0, 0);

            AddBt.Background = Brushes.Orange;
            AddBt.Width = 50;
            AddBt.Height = 30;
            AddBt.HorizontalAlignment = HorizontalAlignment.Center;
            AddBt.FontWeight = FontWeights.Bold;
            AddBt.FontSize = 15;


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

            Grid.SetColumn(Weight, 0);
            Grid.SetRow(Weight, 2);

            Grid.SetColumn(Price, 0);
            Grid.SetRow(Price, 2);

            Grid.SetColumn(AddBt, 1);
            Grid.SetRow(AddBt, 2);


            //Grid album = CreateAlbom(images);




            TempItem.Height = 40;
            TempItem.Children.Add(Im);
            TempItem.Children.Add(Name);
            TempItem.Children.Add(Weight);
            TempItem.Children.Add(Price);
            TempItem.Children.Add(AddBt);
            TempItem.Children.Add(Id);
            //TempItem.Children.Add(counter);
            //TempItem.Children.Add(album);
            TempItem.Width = 170;
            TempItem.Height = 230;
            TempItem.Background = (Brush)(new BrushConverter().ConvertFrom("#FF202020"));
            //TempItem.Background = Brushes.DarkGray;

            return TempItem;
        }

        




        private Grid Counter()
        {

            Grid TempItem = new Grid();
            TempItem.Height = 26;
            TempItem.IsEnabled = false;
            TempItem.HorizontalAlignment = HorizontalAlignment.Right;

            TextBox num = new TextBox();
            num.Text = "0";
            num.Margin = new Thickness(3, 2, 13, 3);
            num.FontSize = 13;
            num.IsReadOnly = true;


            //num.GotFocus += textBox1_GotFocus;

            Button Up = new Button();
            Up.FontSize = 12;
            Up.Padding = new Thickness(0, -4, 0, 0);
            Up.Content = "▲";
            Up.Width = 12;
            Up.Margin = new Thickness(33, 1, 1, 14);
            Up.Click += cmdUp_Click;


            Button Dn = new Button();
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
            choose.VerticalAlignment = VerticalAlignment.Center;
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
            Object parentG = ((sender as Button).Parent as Grid).Children[0];
            TextBox counter = ((sender as Button).Parent as Grid).Children[0] as TextBox;
            counter.IsReadOnly = false;
            counter.Text = (Convert.ToInt32(counter.Text.ToString()) + 1).ToString();
            counter.IsReadOnly = true;
        }

        private void cmdDown_Click(object sender, RoutedEventArgs e)
        {
            TextBox counter = ((sender as Button).Parent as Grid).Children[0] as TextBox;

            if (Convert.ToInt32(counter.Text.ToString()) == 0)
                return;
            counter.IsReadOnly = false;
            counter.Text = (Convert.ToInt32(counter.Text.ToString()) - 1).ToString();
            counter.IsReadOnly = true;

        }

        private void ListBoxItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem Item = (sender as ListBoxItem);
            Grid MainGrid = Item.Content as Grid;
            int childCount = MainGrid.Children.Count;
            int idProduct = Convert.ToInt32((MainGrid.Children[childCount - 1] as TextBlock).Text.ToString());

            var productDTO = data.SelectMany(c => c.Value)
                    .Where(p => p.Key.Id == idProduct)
                    .Select(p => new Dictionary<ProductDTO, List<ProductImageDTO>>() { {p.Key, p.Value } })
                    .FirstOrDefault();

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.pagesFrame.Navigate(new SeparateProductPage(productDTO));
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            var navigationService = (App.Current.MainWindow as MainWindow).pagesFrame.NavigationService;
            navigationService.GoBack();
        }
    }
}

using BLL.ModelsDTO;
using BLL.Services;
using DAL.Data;
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

namespace PizzaUI.Pages
{
    /// <summary>
    /// Interaction logic for BasketPage.xaml
    /// </summary>
    public partial class BasketPage : Page
    {
        ProductService productService;
        ProductImageService productImageService;
        OrderService orderService;
        IList<ProductDTO> productDTOs;
        IList<ProductImageDTO> productImageDTOs;
        UserDTO _user = (App.Current.MainWindow as MainWindow).LoginedUser;
        decimal basket_sum = 0;
        List<BasketDTO> basketItems = new List<BasketDTO>();

        //List<Grid> checkedItems = new List<Grid>();

        Dictionary<BasketDTO, Dictionary<ProductDTO, List<ProductImageDTO>>> data = new Dictionary<BasketDTO, Dictionary<ProductDTO, List<ProductImageDTO>>>();
        public BasketPage()
        {
            InitializeComponent();
            productService = new ProductService();
            productImageService = new ProductImageService();
            orderService = new OrderService();
            setData();
            //FillData();
            loadItems();
        }

        //public BasketPage(BasketDTO basket) : this()
        //{
        //    userbasket = basket.UserId;
        //    //setData();
        //    ////FillData();
        //    //loadItems();
        //}

        private void setData()
        {
            productDTOs = productService.GetAll();
            productImageDTOs = productImageService.GetAll();
            basketItems = orderService.GetBasketsByUserId(_user.Id);
        }

        private async void loadItems()
        {
            foreach(var basketItem in basketItems)
            {
                var basketuserprod = await productService.Find(basketItem.ProductId);

                string img = await productService.GetImg(basketuserprod.Id);

                decimal price = basketuserprod.IsOnDiscount ? basketuserprod.DiscountPrice : basketuserprod.Price;
                Grid item = CreateItem(img, price, basketuserprod.Name, basketuserprod.Id, basketItem.Count, basketItem.ProductId);

                ListBoxBasket.Items.Add(item);

                basket_sum += price * basketItems.Count;
            }


            CreateOrderSum();
        }


        private async void FillData()
        {
            foreach (BasketDTO basket in basketItems)
            {
                data.Add(basket, new Dictionary<ProductDTO, List<ProductImageDTO>>());
            }
            foreach (BasketDTO basket in basketItems)
            {
                //var basketuserprod = await productService.Find(basket.ProductId);
                foreach (ProductDTO productDTO in productDTOs)
                {
                    if (productDTO.Id == basket.ProductId)
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
                            
                            //foreach (var key in data.Keys)
                            //{
                            //    if (key.Id == productDTO.CategoryId)
                            //    {
                            //        data[key].Add(productDTO, tempPhotos);
                            //    }
                            //}

                        }
                    }
                }
            }
        }

        private Grid CreateItem(string img, decimal price, string name, int _id, short count, int productId)
        {

            //List<ProductImageDTO> images = img.OrderBy(i => i.Priority).ToList();

            TextBlock Id = new TextBlock() { Text = _id.ToString() };
            Id.Opacity = 0;
            Id.IsEnabled = false;
            Grid TempItem = new Grid();
            System.Windows.Controls.Image Im = new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(img.ToString())) };
            TextBlock Price = new TextBlock() { Text = price.ToString() + "UAH" };
            TextBlock Name = new TextBlock() { Text = name.ToUpper() };
            TextBlock Count = new TextBlock() { Text = "Count: " + count.ToString() };
            Button DelBt = new Button() { Content = "Delete", Name = $"button_{_id}" };

            Im.Stretch = Stretch.UniformToFill;
            Im.Height = 70;
            Im.Width = 70;
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

            Count.FontSize = 14;
            Count.HorizontalAlignment = HorizontalAlignment.Left;
            Count.VerticalAlignment = VerticalAlignment.Center;
            Count.Foreground = Brushes.White;
            Count.Margin = new Thickness(4, 0, 0, 0);

            DelBt.Background = Brushes.Red;
            DelBt.Width = 40;
            DelBt.Height = 20;
            DelBt.HorizontalAlignment = HorizontalAlignment.Center;
            DelBt.FontWeight = FontWeights.Bold;
            DelBt.FontSize = 12;


            TempItem.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(60) });
            TempItem.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(60) });
            TempItem.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70) });
            TempItem.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(70) });
            TempItem.RowDefinitions.Add(new RowDefinition());
            TempItem.RowDefinitions.Add(new RowDefinition());



            Grid.SetRowSpan(Im, 1);
            Grid.SetColumnSpan(Im, 3);
            Grid.SetColumn(Im, 0);
            Grid.SetRow(Im, 0);

            Grid.SetColumn(Name, 0);
            Grid.SetColumnSpan(Name, 2);
            Grid.SetRow(Name, 1);

            Grid.SetColumn(Count, 0);
            Grid.SetRow(Count, 2);

            Grid.SetColumn(Price, 0);
            Grid.SetRow(Price, 3);

            Grid.SetColumn(DelBt, 1);
            Grid.SetRow(DelBt, 3);

            TempItem.Height = 20;
            TempItem.Children.Add(Im);
            TempItem.Children.Add(Name);
            TempItem.Children.Add(Price);
            TempItem.Children.Add(Count);
            TempItem.Children.Add(DelBt);
            TempItem.Children.Add(Id);
            TempItem.Width = 120;
            TempItem.Height = 200;
            TempItem.Background = (Brush)(new BrushConverter().ConvertFrom("#FF202020"));

            DelBt.Click += delprodbasketClick;

            return TempItem;
        }

        private void CreateOrderSum()
        {
            order_sum_tb.Text += basket_sum;
        }

        private async void delprodbasketClick(object sender, RoutedEventArgs e)
        {
            string name = (sender as Button).Name;
            int prod_id = Int32.Parse(name.Remove(0, 7));
            await orderService.DeleteFromBasket(_user.Id, prod_id);
            order_sum_tb = null;
            basket_sum = 0;
            /*await*/ ListBoxBasket.Items.Clear();
            /*await*/ loadItems();
        }

        private void order_btn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.pagesFrame.Navigate(new MakeOrderPage());
            // basket_sum - зміна в яку записується сума замовлення
        }
    }
}


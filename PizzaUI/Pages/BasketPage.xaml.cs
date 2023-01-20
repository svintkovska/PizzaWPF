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
        CategoryService categoryService;
        ProductImageService productImageService;
        OrderService orderService;
        IList<ProductDTO> productDTOs;
        IList<CategoryDTO> categoryDTOs;
        IList<ProductImageDTO> productImageDTOs;
        IList<BasketDTO> basketDTOs;
        IList<OrderDTO> orderDTOs;
        IList<OrderItemDTO> orderItemDTOs;
        UserDTO _user = (App.Current.MainWindow as MainWindow).LoginedUser;
        EFAppContext context = new EFAppContext();
        int userbasket = 0;
        decimal basket_sum = 0;

        //List<Grid> checkedItems = new List<Grid>();

        Dictionary<BasketDTO, Dictionary<ProductDTO, List<ProductImageDTO>>> data = new Dictionary<BasketDTO, Dictionary<ProductDTO, List<ProductImageDTO>>>();
        public BasketPage()
        {
            InitializeComponent();
            productService = new ProductService();
            categoryService = new CategoryService();
            productImageService = new ProductImageService();
            orderService = new OrderService();
            setData();
            //FillData();
            loadItems();
        }

        public BasketPage(BasketDTO basket) : this()
        {
            userbasket = basket.UserId;
            //setData();
            ////FillData();
            //loadItems();
        }

        private void setData()
        {
            productDTOs = productService.GetAll();
            categoryDTOs = categoryService.GetAll();
            productImageDTOs = productImageService.GetAll();
            //orderDTOs = orderService
            //orderItemDTOs = orderService.GetOrderItemsByOrderId();
        }

        private async void loadItems()
        {
            BasketDTO basket = new BasketDTO();
            //foreach (BasketDTO basket in data.Keys)
            //{
                var basketuserprod = await productService.Find(basket.ProductId);
                if (basket.UserId == userbasket)
                {
                    foreach (ProductDTO product in data[basket].Keys)
                    {
                        Task<string> taskimg = productService.GetImg(basketuserprod.Id);
                        //int i = 1;
                        Grid item = CreateItem(taskimg, basketuserprod.Price, basketuserprod.Name, basketuserprod.Id, basket.Count, basket.ProductId);
                        //if(i==1)
                        //{
                        //    item.Margin = new Thickness(0, 0, 5, 0);
                        //}
                        //else if(i == 2)
                        //{
                        //    item.Margin = new Thickness(5, 0, 0, 0);
                        //}
                        ListBoxBasket.Items.Add(item);

                        basket_sum += basketuserprod.Price;


                        //i += (i == 1) ? -1 : 1;
                    }
                    //break;
                }
            //}
            CreateOrderSum();
        }


        private async void FillData()
        {
            foreach (BasketDTO basket in basketDTOs)
            {
                data.Add(basket, new Dictionary<ProductDTO, List<ProductImageDTO>>());
            }
            foreach (BasketDTO basket in basketDTOs)
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

        private Grid CreateItem(Task<string> img, decimal price, string name, int _id, short count, int productId)
        {

            //List<ProductImageDTO> images = img.OrderBy(i => i.Priority).ToList();

            TextBlock Id = new TextBlock() { Text = _id.ToString() };
            Id.Opacity = 0;
            Id.IsEnabled = false;
            Grid TempItem = new Grid();
            System.Windows.Controls.Image Im = new System.Windows.Controls.Image() { Source = new BitmapImage(new Uri(img.ToString())) };
            TextBlock Price = new TextBlock() { Text = price.ToString() + "UAH" };
            TextBlock Name = new TextBlock() { Text = name.ToUpper() };
            TextBlock Count = new TextBlock() { Text = count.ToString() };
            Button DelBt = new Button() { Content = "Delete", Name = $"button_{_id}" };

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

            Count.FontSize = 14;
            Count.HorizontalAlignment = HorizontalAlignment.Left;
            Count.VerticalAlignment = VerticalAlignment.Center;
            Count.Foreground = Brushes.White;
            Count.Margin = new Thickness(4, 0, 0, 0);

            DelBt.Background = Brushes.Red;
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
            TempItem.Children.Add(Count);
            TempItem.Children.Add(DelBt);
            TempItem.Children.Add(Id);
            TempItem.Width = 170;
            TempItem.Height = 230;
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
        }

        private void order_btn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.pagesFrame.Navigate(new MakeOrderPage());
            // basket_sum - зміна в яку записується сума замовлення
        }
    }
}


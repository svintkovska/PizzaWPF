using BLL.ModelsDTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        UserDTO user = new UserDTO();
        UserService _userService = new UserService();
        ProductService _productService = new ProductService();
        OrderService _orderService = new OrderService();
        public UserPage()
        {
            InitializeComponent();
            user = (App.Current.MainWindow as MainWindow).LoginedUser;
            if (user != null)
            {
                userName_tb.Text = user.FirstName;
                userLastName_tb.Text = user.LastName;
                userEmail_tb.Text = user.Email;
                userPhone_tb.Text = user.Phone;

                FillOrderHistory();
            }

        }
        private readonly Regex validateEmailRegex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
        private readonly Regex validatePhoneNumberRegex = new Regex("^[+][3][8][0][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]$");

        private async void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(userName_tb.Text))
                user.FirstName = userName_tb.Text;
            if (!String.IsNullOrEmpty(userLastName_tb.Text))
                user.LastName = userLastName_tb.Text;
            if (!String.IsNullOrEmpty(userEmail_tb.Text) && validateEmailRegex.IsMatch(userEmail_tb.Text))
                user.Email = userEmail_tb.Text;
            if (!String.IsNullOrEmpty(userPhone_tb.Text) && validatePhoneNumberRegex.IsMatch(userPhone_tb.Text))
                user.Phone = userPhone_tb.Text;

            await _userService.Update(user.Id, user);

        }

        private async void changePassBtn_Click(object sender, RoutedEventArgs e)
        {
            string newPassword = " ";
            using (MD5 md5Hash = MD5.Create())
            {
                newPassword = GetMd5Hash(md5Hash, userNewPass_tb.Password);
            }

            user.Password = newPassword;
            await _userService.Update(user.Id, user);


            userOldPass_tb.Password = "";
            userNewPass_tb.Password = "";
            userConfirmPass_tb.Password = "";

        }
        private void passwordGrid_MouseMove(object sender, MouseEventArgs e)
        {
            ValidateNewPassword();
        }
        private void ValidateNewPassword()
        {

            string oldPassword = " ";
            string userPassword = user.Password;
            using (MD5 md5Hash = MD5.Create())
            {
                oldPassword = GetMd5Hash(md5Hash, userOldPass_tb.Password);
            }

            if(oldPassword != userPassword)
                changePassBtn.IsEnabled = false;
            else if (String.IsNullOrEmpty(userNewPass_tb.Password))
                changePassBtn.IsEnabled = false;
            else if(String.IsNullOrEmpty(userConfirmPass_tb.Password))
                changePassBtn.IsEnabled = false;
            else if(userNewPass_tb.Password != userConfirmPass_tb.Password)
                changePassBtn.IsEnabled = false;
            else
                changePassBtn.IsEnabled = true;
        }

        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }

                        //<ListBoxItem Height = "100" Background="DarkRed" Focusable="False">
                        //    <Grid >
                        //        <Grid.ColumnDefinitions>
                        //            <ColumnDefinition MinWidth = "70" />
                        //            < ColumnDefinition MinWidth="100"/>
                        //            <ColumnDefinition MinWidth = "100" />
                        //            < ColumnDefinition MinWidth="100"/>
                        //            <ColumnDefinition MinWidth = "250" />
                        //            < ColumnDefinition MinWidth="100"/>
                        //        </Grid.ColumnDefinitions >
                        //        <Grid.RowDefinitions >
                        //            <RowDefinition Height = "0.5*" />
                        //            < RowDefinition />
                        //        </ Grid.RowDefinitions >
                        //        < Label Grid.Column="0" Grid.Row= "0" FontSize= "17" Margin= "10 0 20 0" >Nº</Label>
                        //        <Label Grid.Column="1" Grid.Row= "0" FontSize= "17" Margin= "10 0 20 0" > Date:</Label>
                        //        <Label Grid.Column= "2" Grid.Row= "0"  FontSize= "17" Margin= "0 0 20 0" > Price:</Label>
                        //        <Label Grid.Column= "3" Grid.Row= "0" FontSize= "17" Margin= "0 0 20 0" > Amount:</Label>
                        //        <Label Grid.Column= "4" Grid.Row= "0"  FontSize= "17" Margin= "0" > Name:</Label>
                        //        <Label Grid.Column= "5" Grid.Row= "0"  FontSize= "17" Margin= "0" > Total:</Label>

                        //        <TextBlock Grid.Column= "0" Grid.Row= "1" Foreground= "Black" Margin= "10 0" HorizontalAlignment= "Center" VerticalAlignment= "Center" > 112 </ TextBlock >
                        //        < Grid Grid.Column= "1" Grid.Row= "1" >
                        //            < Grid.RowDefinitions >
                        //                < RowDefinition />
                        //                < RowDefinition />
                        //            </ Grid.RowDefinitions >
                        //            < TextBlock Grid.Row= "0" Foreground= "Black" Margin= "10 0" HorizontalAlignment= "Center" VerticalAlignment= "Center" > 19.01.23</TextBlock>
                        //            <TextBlock Grid.Row= "1" Foreground= "Black" Margin= "10 0" HorizontalAlignment= "Center" VerticalAlignment= "Center" > 15:00:00</TextBlock>

                        //        </Grid>
                        //        <TextBlock Grid.Column= "2" Grid.Row= "1" Foreground= "Black" Margin= "0" HorizontalAlignment= "Center" VerticalAlignment= "Center" > 1250 </ TextBlock >
                        //        < TextBlock Grid.Column= "3" Grid.Row= "1" Foreground= "Black" Margin= "0" HorizontalAlignment= "Center" VerticalAlignment= "Center" > 19 </ TextBlock >
                        //        < Image Grid.Column= "4" Grid.Row= "2" Width= "70" Height= "70"
                        //               Source= "https://upload.wikimedia.org/wikipedia/commons/thumb/a/a3/Eq_it-na_pizza-margherita_sep2005_sml.jpg/800px-Eq_it-na_pizza-margherita_sep2005_sml.jpg" ></ Image >
                        //        < TextBlock Grid.Column= "5" Grid.Row= "1" Foreground= "Black" Margin= "0" HorizontalAlignment= "Center" VerticalAlignment= "Center" > 190 </ TextBlock >

                        //    </ Grid >
                        //</ ListBoxItem >
         private async void FillOrderHistory()
         {
            List<OrderDTO> history =  _orderService.GetOrderHistory(user.Id);

            List<OrderItemDTO> orderItems = new List<OrderItemDTO>();
            foreach (var order in history)
            {
                foreach(var item in _orderService.GetOrderItemsByOrderId(order.Id))
                {
                    orderItems.Add(item);
                }
            }


            Label noHistoryText = new Label() { Content = "No history found.", FontSize = 30, Margin = new Thickness(50, 50, 20, 0), HorizontalAlignment = HorizontalAlignment.Center };


            if (orderItems.Count < 1)
            {
                orderHistoryListBox.Items.Add(noHistoryText);
            }
            else
            {
                foreach (var item in orderItems)
                {
                    orderHistoryListBox.Items.Remove(noHistoryText);


                    var product = await _productService.Find(item.ProductId);

                    ListBoxItem listBoxItem = new ListBoxItem() { Height = 100, Background = Brushes.DarkRed, Focusable = false };
                    Grid grid = new Grid();

                    grid.RowDefinitions.Add(new RowDefinition() { Height = new GridLength(0.5, GridUnitType.Star) });
                    grid.RowDefinitions.Add(new RowDefinition());

                    grid.ColumnDefinitions.Add(new ColumnDefinition() { MinWidth = 70 });
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { MinWidth = 100 });
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { MinWidth = 100 });
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { MinWidth = 100 });
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { MinWidth = 250 });
                    grid.ColumnDefinitions.Add(new ColumnDefinition() { MinWidth = 100 });


                    Label labelNumber = new Label() { Content = "Nº", FontSize = 17, Margin = new Thickness(10, 0, 20, 0), HorizontalAlignment = HorizontalAlignment.Center };
                    Label labelDate = new Label() { Content = "Date:", FontSize = 17, Margin = new Thickness(0, 0, 20, 0), HorizontalAlignment = HorizontalAlignment.Center };

                    Label labelPrice = new Label() { Content = "Price:", FontSize = 17, Margin = new Thickness(0, 0, 20, 0), HorizontalAlignment = HorizontalAlignment.Center };
                    Label labelAmount = new Label() { Content = "Amount:", FontSize = 17, Margin = new Thickness(0, 0, 20, 0), HorizontalAlignment = HorizontalAlignment.Center };
                    Label labelName = new Label() { Content = product.Name, FontSize = 17, Margin = new Thickness(0, 0, 20, 0), HorizontalAlignment = HorizontalAlignment.Center };
                    Label labelTotal = new Label() { Content = "Total:", FontSize = 17, Margin = new Thickness(0, 0, 20, 0), HorizontalAlignment = HorizontalAlignment.Center };


                    TextBlock numberText = new TextBlock()
                    {
                        Text = item.Id.ToString(),
                        Foreground = Brushes.Black,
                        Margin = new Thickness(10, 0, 10, 0),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };


                    Grid subGrid = new Grid();
                    subGrid.RowDefinitions.Add(new RowDefinition());
                    subGrid.RowDefinitions.Add(new RowDefinition());
                    TextBlock dateText1 = new TextBlock()
                    {
                        Text = item.DateCreated.ToShortDateString(),
                        Foreground = Brushes.Black,
                        Margin = new Thickness(10, 0, 10, 0),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    TextBlock dateText2 = new TextBlock()
                    {
                        Text = item.DateCreated.ToShortTimeString(),
                        Foreground = Brushes.Black,
                        Margin = new Thickness(10, 0, 10, 0),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    Grid.SetRow(dateText1, 0);
                    subGrid.Children.Add(dateText1);
                    Grid.SetRow(dateText2, 1);
                    subGrid.Children.Add(dateText2);




                    TextBlock priceText = new TextBlock()
                    {
                        Text = item.Price.ToString(),
                        Foreground = Brushes.Black,
                        Margin = new Thickness(0),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    TextBlock amountText = new TextBlock()
                    {
                        Text = item.Count.ToString(),
                        Foreground = Brushes.Black,
                        Margin = new Thickness(0),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };
                    decimal totalPrice = item.Count * item.Price;
                    TextBlock totalText = new TextBlock()
                    {
                        Text = totalPrice.ToString(),
                        Foreground = Brushes.Black,
                        Margin = new Thickness(0),
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center
                    };


                    Image img = new Image() { Width = 70, Height = 60, Stretch = Stretch.UniformToFill, VerticalAlignment = VerticalAlignment.Center };
                    BitmapImage bmp = new BitmapImage();
                    string someUrl = await _productService.GetImg(item.ProductId);
                    using (var webClient = new WebClient())
                    {
                        try
                        {
                            byte[] imageBytes = webClient.DownloadData(someUrl);
                            bmp = ToBitmapImage(imageBytes);
                        }
                        catch
                        {

                        }

                    }
                    img.Source = bmp;

                    Grid.SetRow(labelNumber, 0);
                    Grid.SetColumn(labelNumber, 0);
                    grid.Children.Add(labelNumber);
                    Grid.SetRow(labelDate, 0);
                    Grid.SetColumn(labelDate, 1);
                    grid.Children.Add(labelDate);
                    Grid.SetRow(labelPrice, 0);
                    Grid.SetColumn(labelPrice, 2);
                    grid.Children.Add(labelPrice);
                    Grid.SetRow(labelAmount, 0);
                    Grid.SetColumn(labelAmount, 3);
                    grid.Children.Add(labelAmount);
                    Grid.SetRow(labelName, 0);
                    Grid.SetColumn(labelName, 4);
                    grid.Children.Add(labelName);
                    Grid.SetRow(labelTotal, 0);
                    Grid.SetColumn(labelTotal, 5);
                    grid.Children.Add(labelTotal);

                    Grid.SetRow(numberText, 1);
                    Grid.SetColumn(numberText, 0);
                    grid.Children.Add(numberText);
                    Grid.SetRow(subGrid, 1);
                    Grid.SetColumn(subGrid, 1);
                    grid.Children.Add(subGrid);
                    Grid.SetRow(priceText, 1);
                    Grid.SetColumn(priceText, 2);
                    grid.Children.Add(priceText);
                    Grid.SetRow(amountText, 1);
                    Grid.SetColumn(amountText, 3);
                    grid.Children.Add(amountText);
                    Grid.SetRow(img, 1);
                    Grid.SetColumn(img, 4);
                    grid.Children.Add(img);
                    Grid.SetRow(totalText, 1);
                    Grid.SetColumn(totalText, 5);
                    grid.Children.Add(totalText);

                    listBoxItem.Content = grid;
                    orderHistoryListBox.Items.Add(listBoxItem);

                }
            }


            

           
        }

        private BitmapImage ToBitmapImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {

                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;
                img.StreamSource = ms;
                img.EndInit();

                if (img.CanFreeze)
                {
                    img.Freeze();
                }
                return img;
            }
        }
    }
}

using BLL.ModelsDTO;
using BLL.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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
    /// Interaction logic for CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {
        CategoryService categoryService = new CategoryService();
        public CategoriesPage()
        {
            InitializeComponent();
            GenerateCategories();
        }

         //<Border>
         //               <Button Background = "Transparent" >
         //                   < Grid >
         //                       < Grid.RowDefinitions >
         //                           < RowDefinition />
         //                           < RowDefinition />
         //                       </ Grid.RowDefinitions >
         //                       < Image Source = "https://ternopil.celentano.delivery/wp-content/uploads/2019/06/bavarska-538x538.jpg" ></Image >
         //                       <Label Grid.Row="1" Margin= "0" HorizontalAlignment= "Center" > Pizza1 </ Label >
         //                   </ Grid >
         //               </ Button >


         //           </ Border >


        private void GenerateCategories()
        {
            CategoryService categoryService = new CategoryService();
            var list = categoryService.GetAll();


            for (int i = 0; i < list.Count; i++)
            {
                if(!list[i].IsDelete)
                {
                    var border = new Border();
                    var grid = new Grid();
                    var button = new Button();
                    button.Name = $"button_{list[i].Id}";
                    button.Background = Brushes.Transparent;
                    

                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.RowDefinitions.Add(new RowDefinition());

                    var image = new Image();
                    BitmapImage bmp = new BitmapImage();
                    string someUrl = list[i].Image;
                    using (var webClient = new WebClient())
                    {
                        byte[] imageBytes = webClient.DownloadData(someUrl);
                        bmp = ToBitmapImage(imageBytes);
                    }
                    image.Source = bmp;

                    var label = new Label();
                    label.Content = list[i].Name;

                    Grid.SetRow(image, 0);
                    grid.Children.Add(image);

                    Grid.SetRow(label, 1);
                    grid.Children.Add(label);
                    button.Content = grid;
                    border.Child = button;
                    wrapPanel.Children.Add(border);

                    button.Click += myClick;
                }
               
            }
        }

        private async void myClick(object sender, RoutedEventArgs e)
        {
            string name = (sender as Button).Name;
            var id = name.Remove(0,7);

            CategoryDTO category = await categoryService.Find(Int32.Parse(id));
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.pagesFrame.Navigate(new AllProducts(category));
            //mainWindow.pagesFrame.Navigate(new SeparateProductPage());

        }
        private BitmapImage ToBitmapImage(byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {

                BitmapImage img = new BitmapImage();
                img.BeginInit();
                img.CacheOption = BitmapCacheOption.OnLoad;//CacheOption must be set after BeginInit()
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

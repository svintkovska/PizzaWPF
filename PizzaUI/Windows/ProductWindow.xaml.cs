using BLL.ModelsDTO;
using BLL.Services;
using DAL.Data;
using DAL.Data.Entities;
using Microsoft.Win32;
using PizzaUI.ImgToServer;
using PizzaUI.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace PizzaUI.Windows
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        EFAppContext context = new EFAppContext();
        private ProductService productService;
        private ProductImageService productImgService;

        private string base64AddImg1;
        private string base64AddImg2;
        private string base64AddImg3;

        private string base64UpdImg1;
        private string base64UpdImg2;
        private string base64UpdImg3;
        public ProductWindow()
        {
            InitializeComponent();
            productService = new ProductService();
            productImgService = new ProductImageService();
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            ValidateCreate();
            ValidateDelete();
            ValidateEdit();
        }


        private void delete_prod_btn_Click_1(object sender, RoutedEventArgs e)
        {
            var product = comboboxDel.SelectedItem as ProductDTO;
            productService.Delete(product.Id);
            comboboxDel.SelectedIndex = -1;

            var cm = DataContext as CategoriesVM;
            cm.GetProducts.Remove(product);
        }

        private async void add_prod_btn_Click(object sender, RoutedEventArgs e)
        {
            var category = comboboxAdd.SelectedItem as CategoryDTO;
            var discount = false;
            if (isondiscount_add.IsChecked == true)
            {
                discount = true;
            }
            else
            {
                discount = false;
            }

            var new_product = new ProductDTO
            {
                Name = name_add.Text,
                Price = decimal.Parse(price_add.Text),
                DiscountPrice = decimal.Parse(discount_price_add.Text),
                IsOnDiscount = discount,
                Weight = Int32.Parse(weight_add.Text),
                Description = description_add.Text,
                CategoryId = category.Id,
                DateCreated = DateTime.Now,
                IsDelete = false
            };

            //int id = productService.Create(new_product);

            //var findprod = context.Products.FirstOrDefault(x => x.Name == name_add.Text);
            //findprod.Id

            //////////////////////////////////// треба якось витягнути id продукта, який створили
            ///
            var id = await productService.Create(new_product);

            if (!String.IsNullOrEmpty(base64AddImg1))
            {
                await productImgService.Create(new ProductImageDTO
                {
                    Name = base64AddImg1,
                    Priority = 1,
                    ProductId = id,
                    DateCreated = DateTime.Now,
                    IsDelete = false
                });
            }
            if (!String.IsNullOrEmpty(base64AddImg2))
            {
                await productImgService.Create(new ProductImageDTO
                {
                    Name = base64AddImg2,
                    Priority = 2,
                    ProductId = id,
                    DateCreated = DateTime.Now,
                    IsDelete = false
                });
            }
            if (!String.IsNullOrEmpty(base64AddImg3))
            {
                await productImgService.Create(new ProductImageDTO
                {
                    Name = base64AddImg3,
                    Priority = 3,
                    ProductId = id,
                    DateCreated = DateTime.Now,
                    IsDelete = false
                });
            }

            name_add.Text = "";
            price_add.Text = "";
            discount_price_add.Text = "";
            isondiscount_add.IsChecked = false;
            weight_add.Text = "";
            description_add.Text = "";

            imgBackgrAdd1.Background = Brushes.Transparent;
            imgBackgrAdd2.Background = Brushes.Transparent;
            imgBackgrAdd3.Background = Brushes.Transparent;
            base64AddImg1 = null;
            base64AddImg2 = null;
            base64AddImg3 = null;
            editPhotoAdd1.Visibility = Visibility.Hidden;
            editPhotoAdd2.Visibility = Visibility.Hidden;
            editPhotoAdd3.Visibility = Visibility.Hidden;
            delPhotoAdd1.Visibility = Visibility.Hidden;
            delPhotoAdd2.Visibility = Visibility.Hidden;
            delPhotoAdd3.Visibility = Visibility.Hidden;

            var cm = DataContext as CategoriesVM;
            cm.GetProducts.Add(new_product);
        }

     
        private void edit_prod_btn_Click(object sender, RoutedEventArgs e)
        {
            decimal d = 0;
            int i = 0;
            var product = comboboxProductUpd.SelectedItem as ProductDTO;
            var category = comboboxCategoryUpd.SelectedItem as CategoryDTO;
            var discount = false;
            if (new_isondiscount.IsChecked == true)
            {
                discount = true;
            }
            else
            {
                discount = false;
            }

            if (!String.IsNullOrEmpty(new_name.Text))
                product.Name = new_name.Text;
            if (decimal.TryParse(new_price.Text, out d))
                product.Price = decimal.Parse(new_price.Text);
            if (decimal.TryParse(new_discount_price.Text, out d))
                product.DiscountPrice = decimal.Parse(new_discount_price.Text);
            product.IsOnDiscount = discount;
            if (Int32.TryParse(new_weight.Text, out i))
                product.Weight = Int32.Parse(new_weight.Text);
            if (!String.IsNullOrEmpty(new_description.Text))
                product.Description = new_description.Text;
            if (category != null)
                product.CategoryId = category.Id;
            
            productService.Update(product.Id, product);

            new_name.Text = "";
            new_price.Text = "";
            new_discount_price.Text = "";
            new_isondiscount.IsChecked = false;
            new_weight.Text = "";
            new_description.Text = "";

            imgBackgrUpd1.Background = Brushes.Transparent;
            imgBackgrUpd2.Background = Brushes.Transparent;
            imgBackgrUpd3.Background = Brushes.Transparent;
            base64UpdImg1 = null;
            base64UpdImg2 = null;
            base64UpdImg3 = null;
            editPhotoUpd1.Visibility = Visibility.Hidden;
            editPhotoUpd2.Visibility = Visibility.Hidden;
            editPhotoUpd3.Visibility = Visibility.Hidden;
            delPhotoUpd1.Visibility = Visibility.Hidden;
            delPhotoUpd2.Visibility = Visibility.Hidden;
            delPhotoUpd3.Visibility = Visibility.Hidden;

            comboboxProductUpd.SelectedIndex = -1;
        }


        private void ValidateCreate()
        {
            decimal d = 0;
            int i = 0;
            var category = comboboxAdd.SelectedItem as CategoryDTO;

            if (String.IsNullOrEmpty(name_add.Text))
                add_prod_btn.IsEnabled = false;
            else if (!decimal.TryParse(price_add.Text, out d))
                add_prod_btn.IsEnabled = false;
            else if (!decimal.TryParse(discount_price_add.Text, out d))
                add_prod_btn.IsEnabled = false;
            else if (!Int32.TryParse(weight_add.Text, out i))
                add_prod_btn.IsEnabled = false;
            else if (String.IsNullOrEmpty(description_add.Text))
                add_prod_btn.IsEnabled = false;
            else if (category == null)
                add_prod_btn.IsEnabled = false;
            else if (base64AddImg1 == null && base64AddImg2 == null && base64AddImg3 == null)
                add_prod_btn.IsEnabled = false;
            else
                add_prod_btn.IsEnabled = true;
        }

        private void ValidateEdit()
        {
            var product = comboboxProductUpd.SelectedItem as ProductDTO;
            if (product == null)
                edit_prod_btn.IsEnabled = false;
            else
                edit_prod_btn.IsEnabled = true;

        }
        private void ValidateDelete()
        {
            var product = comboboxDel.SelectedItem as ProductDTO;
            if (product == null)
                delete_prod_btn.IsEnabled = false;
            else
                delete_prod_btn.IsEnabled = true;
        }


        /// //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        private void addImg1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                string pathAddImg = fileDialog.FileName;

                byte[] imageBytes = File.ReadAllBytes(pathAddImg);

                base64AddImg1 = Convert.ToBase64String(imageBytes);

                base64AddImg1 = UploadImagesToServer.UploadImage(base64AddImg1);
                imgBackgrAdd1.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), base64AddImg1)));
                editPhotoAdd1.Visibility = Visibility.Visible;
                delPhotoAdd1.Visibility = Visibility.Visible;
            }
        }

        private void addImg2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                string pathAddImg = fileDialog.FileName;

                byte[] imageBytes = File.ReadAllBytes(pathAddImg);

                base64AddImg2 = Convert.ToBase64String(imageBytes);

                base64AddImg2 = UploadImagesToServer.UploadImage(base64AddImg2);
                imgBackgrAdd2.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), base64AddImg2)));
                editPhotoAdd2.Visibility = Visibility.Visible;
                delPhotoAdd2.Visibility = Visibility.Visible;
            }
        }

        private void addImg3_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                string pathAddImg = fileDialog.FileName;

                byte[] imageBytes = File.ReadAllBytes(pathAddImg);

                base64AddImg3 = Convert.ToBase64String(imageBytes);

                base64AddImg3 = UploadImagesToServer.UploadImage(base64AddImg3);
                imgBackgrAdd3.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), base64AddImg3)));
                editPhotoAdd3.Visibility = Visibility.Visible;
                delPhotoAdd3.Visibility = Visibility.Visible;
            }
        }

        private void editPhotoAdd1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                string pathAddImg = fileDialog.FileName;

                byte[] imageBytes = File.ReadAllBytes(pathAddImg);

                base64AddImg1 = Convert.ToBase64String(imageBytes);

                base64AddImg1 = UploadImagesToServer.UploadImage(base64AddImg1);
                imgBackgrAdd1.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), base64AddImg1)));
            }
        }

        private void delPhotoAdd1_Click(object sender, RoutedEventArgs e)
        {
            imgBackgrAdd1.Background = Brushes.Transparent;
            base64AddImg1 = null;
            editPhotoAdd1.Visibility = Visibility.Hidden;
            delPhotoAdd1.Visibility = Visibility.Hidden;
        }

        private void editPhotoAdd2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                string pathAddImg = fileDialog.FileName;

                byte[] imageBytes = File.ReadAllBytes(pathAddImg);

                base64AddImg2 = Convert.ToBase64String(imageBytes);

                base64AddImg2 = UploadImagesToServer.UploadImage(base64AddImg2);
                imgBackgrAdd2.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), base64AddImg2)));
            }
        }

        private void delPhotoAdd2_Click(object sender, RoutedEventArgs e)
        {
            imgBackgrAdd2.Background = Brushes.Transparent;
            base64AddImg2 = null;
            editPhotoAdd2.Visibility = Visibility.Hidden;
            delPhotoAdd2.Visibility = Visibility.Hidden;
        }

        private void editPhotoAdd3_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                string pathAddImg = fileDialog.FileName;

                byte[] imageBytes = File.ReadAllBytes(pathAddImg);

                base64AddImg3 = Convert.ToBase64String(imageBytes);

                base64AddImg3 = UploadImagesToServer.UploadImage(base64AddImg3);
                imgBackgrAdd3.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), base64AddImg3)));
            }
        }

        private void delPhotoAdd3_Click(object sender, RoutedEventArgs e)
        {
            imgBackgrAdd3.Background = Brushes.Transparent;
            base64AddImg3 = null;
            editPhotoAdd3.Visibility = Visibility.Hidden;
            delPhotoAdd3.Visibility = Visibility.Hidden;
        }

        private void updImg1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                string pathUpdImg = fileDialog.FileName;

                byte[] imageBytes = File.ReadAllBytes(pathUpdImg);

                base64UpdImg1 = Convert.ToBase64String(imageBytes);

                base64UpdImg1 = UploadImagesToServer.UploadImage(base64UpdImg1);
                imgBackgrUpd1.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), base64UpdImg1)));
                editPhotoUpd1.Visibility = Visibility.Visible;
                delPhotoUpd1.Visibility = Visibility.Visible;
            }
        }

        private void updImg2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                string pathUpdImg = fileDialog.FileName;

                byte[] imageBytes = File.ReadAllBytes(pathUpdImg);

                base64UpdImg2 = Convert.ToBase64String(imageBytes);

                base64UpdImg2 = UploadImagesToServer.UploadImage(base64UpdImg2);
                imgBackgrUpd2.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), base64UpdImg2)));
                editPhotoUpd2.Visibility = Visibility.Visible;
                delPhotoUpd2.Visibility = Visibility.Visible;
            }
        }

        private void updImg3_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                string pathUpdImg = fileDialog.FileName;

                byte[] imageBytes = File.ReadAllBytes(pathUpdImg);

                base64UpdImg3 = Convert.ToBase64String(imageBytes);

                base64UpdImg3 = UploadImagesToServer.UploadImage(base64UpdImg3);
                imgBackgrUpd3.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), base64UpdImg3)));
                editPhotoUpd3.Visibility = Visibility.Visible;
                delPhotoUpd3.Visibility = Visibility.Visible;
            }
        }
        private void editPhotoUpd1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                string pathUpdImg = fileDialog.FileName;

                byte[] imageBytes = File.ReadAllBytes(pathUpdImg);

                base64UpdImg1 = Convert.ToBase64String(imageBytes);

                base64UpdImg1 = UploadImagesToServer.UploadImage(base64UpdImg1);
                imgBackgrUpd1.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), base64UpdImg1)));
            }
        }

        private void delPhotoUpd1_Click(object sender, RoutedEventArgs e)
        {
            imgBackgrUpd1.Background = Brushes.Transparent;
            base64UpdImg1 = null;
            editPhotoUpd1.Visibility = Visibility.Hidden;
            delPhotoUpd1.Visibility = Visibility.Hidden;
        }

        private void editPhotoUpd2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                string pathUpdImg = fileDialog.FileName;

                byte[] imageBytes = File.ReadAllBytes(pathUpdImg);

                base64UpdImg2 = Convert.ToBase64String(imageBytes);

                base64UpdImg2 = UploadImagesToServer.UploadImage(base64UpdImg2);
                imgBackgrUpd2.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), base64UpdImg2)));
            }
        }

        private void delPhotoUpd2_Click(object sender, RoutedEventArgs e)
        {
            imgBackgrUpd2.Background = Brushes.Transparent;
            base64UpdImg2 = null;
            editPhotoUpd2.Visibility = Visibility.Hidden;
            delPhotoUpd2.Visibility = Visibility.Hidden;
        }

        private void editPhotoUpd3_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                string pathUpdImg = fileDialog.FileName;

                byte[] imageBytes = File.ReadAllBytes(pathUpdImg);

                base64UpdImg3 = Convert.ToBase64String(imageBytes);

                base64UpdImg3 = UploadImagesToServer.UploadImage(base64UpdImg3);
                imgBackgrUpd3.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), base64UpdImg3)));
            }
        }

        private void delPhotoUpd3_Click(object sender, RoutedEventArgs e)
        {
            imgBackgrUpd3.Background = Brushes.Transparent;
            base64UpdImg3 = null;
            editPhotoUpd3.Visibility = Visibility.Hidden;
            delPhotoUpd3.Visibility = Visibility.Hidden;
        }

        private void order_btn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

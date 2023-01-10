using BLL.ModelsDTO;
using BLL.Services;
using DAL.Data;
using DAL.Data.Entities;
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
using System.Windows.Shapes;

namespace PizzaUI.Windows
{
    /// <summary>
    /// Interaction logic for ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        EFAppContext dataContext = new EFAppContext();
        ProductService productService = new ProductService();
        int prod_id;
        bool isdiscount;
        public ProductWindow()
        {
            InitializeComponent();
        }

        public void Find_Prod_By_Name()
        {
            var product = dataContext.Products.FirstOrDefault(x => x.Name == prod_name.Text);

            if (new_isondiscount.Text == "true" || new_isondiscount.Text == "false")
            {
                isdiscount = true;
            }

            if (product != null)
            {
                prod_id = product.Id;
            }
            else
            {
                MessageBox.Show("Error name of product");
            }
        }

        public bool Check_for_null_textbox()
        {
            if (new_name.Text != null && new_price.Text != null && new_discount_price.Text != null
                    && new_isondiscount.Text != null && new_weight.Text != null && new_description != null
                    && new_category_id != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public void Check_textbox_for_Edit()
        //{
        //    if (new_name.Text == null) { new_name.Text = dataContext.Products.FirstOrDefault(x => x.Name == prod_name.Text)}
        //}

        private void delete_prod_btn_Click(object sender, RoutedEventArgs e)
        {
            Find_Prod_By_Name();
            productService.Delete(prod_id);
        }

        private void add_prod_btn_Click(object sender, RoutedEventArgs e)
        {
            Find_Prod_By_Name();
            if (isdiscount == true)
            {
                var new_product = new ProductDTO
                {
                    Name = new_name.Text,
                    Price = decimal.Parse(new_price.Text),
                    DiscountPrice = decimal.Parse(new_discount_price.Text),
                    IsOnDiscount = bool.Parse(new_isondiscount.Text),
                    Weight = Int32.Parse(new_weight.Text),
                    Description = new_description.Text,
                    CategoryId = Int32.Parse(new_category_id.Text),
                    DateCreated = DateTime.Now,
                    IsDelete = false
                };
                if (Check_for_null_textbox() == true)
                {
                    productService.Create(new_product);
                }
                else
                {
                    MessageBox.Show("Error. Not all textboxes have text!");
                }
            }
            else
            {
                MessageBox.Show("Error isondiscount!");
            }
        }

        private void edit_prod_btn_Click(object sender, RoutedEventArgs e)
        {
            Find_Prod_By_Name();
            if (isdiscount == true)
            {
                var new_product = new ProductDTO
                {
                    Name = new_name.Text,
                    Price = decimal.Parse(new_price.Text),
                    DiscountPrice = decimal.Parse(new_discount_price.Text),
                    IsOnDiscount = bool.Parse(new_isondiscount.Text),
                    Weight = Int32.Parse(new_weight.Text),
                    Description = new_description.Text,
                    CategoryId = Int32.Parse(new_category_id.Text),
                    DateCreated = DateTime.Now,
                    IsDelete = false
                };
                productService.Update(new_product);
            }
            else
            {
                MessageBox.Show("Error isondiscount!");
            }
        }
    }
}

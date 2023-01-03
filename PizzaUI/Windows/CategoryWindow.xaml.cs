using BLL.ModelsDTO;
using BLL.Services;
using Microsoft.Win32;
using PizzaUI.ViewModels;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CategoryWindow.xaml
    /// </summary>
    public partial class CategoryWindow : Window
    {
        private CategoryService categoryService;
        private string pathUpdImg;
        
        public CategoryWindow()
        {
            InitializeComponent();

            categoryService = new CategoryService();
        }

        private void selectImgUpdBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                pathUpdImg = fileDialog.FileName;
                imgBackgrUpd.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), pathUpdImg)));
                editPhotoUpd.Visibility = Visibility.Visible;
                delPhotoUpd.Visibility = Visibility.Visible;
            }
        }

        private void editPhotoUpd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                pathUpdImg = fileDialog.FileName;
                imgBackgrUpd.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), pathUpdImg)));
            }
        }

        private void delPhotoUpd_Click(object sender, RoutedEventArgs e)
        {
            imgBackgrUpd.Background = Brushes.Transparent;
            pathUpdImg = null;
            editPhotoUpd.Visibility = Visibility.Hidden;
            delPhotoUpd.Visibility = Visibility.Hidden;
        }

        private void updateBtn_Click(object sender, RoutedEventArgs e)
        {
            var category = comboboxUpd.SelectedItem as CategoryDTO;

            if (!String.IsNullOrEmpty(nameUpd.Text))
            {
                category.Name = nameUpd.Text;
            }
            if (!String.IsNullOrEmpty(pathUpdImg))
            {
                category.Image = pathUpdImg;
            }
            categoryService.Update(category);

            imgBackgrUpd.Background = Brushes.Transparent;
            pathUpdImg = null;
            editPhotoUpd.Visibility = Visibility.Hidden;
            delPhotoUpd.Visibility = Visibility.Hidden;
            nameUpd.Text = "";
            comboboxUpd.SelectedIndex = -1;

            CategoriesVM vm = new CategoriesVM();
            comboboxUpd.ItemsSource = vm.GetCategories;
        }

       
    }
}

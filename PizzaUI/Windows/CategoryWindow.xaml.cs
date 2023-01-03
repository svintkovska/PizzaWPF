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
        private string pathAddImg;
        
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

            CategoriesVM vm = new CategoriesVM();
            comboboxUpd.ItemsSource = vm.GetCategories;
            comboboxDel.ItemsSource = vm.GetCategories;

            comboboxUpd.SelectedIndex = -1;

        }

        private void selectImgAddBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                pathAddImg = fileDialog.FileName;
                imgBackgrAdd.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), pathAddImg)));
                editPhotoAdd.Visibility = Visibility.Visible;
                delPhotoAdd.Visibility = Visibility.Visible;
            }
        }

        private void editPhotoAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                pathAddImg = fileDialog.FileName;
                imgBackgrAdd.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), pathAddImg)));
            }
        }

        private void delPhotoAdd_Click(object sender, RoutedEventArgs e)
        {
            imgBackgrAdd.Background = Brushes.Transparent;
            pathAddImg = null;
            editPhotoAdd.Visibility = Visibility.Hidden;
            delPhotoAdd.Visibility = Visibility.Hidden;
        }

        private void addBtn_Click(object sender, RoutedEventArgs e)
        {
            CategoryDTO category = new CategoryDTO();
            if (!String.IsNullOrEmpty(nameAdd.Text))
            {
                category.Name = nameAdd.Text;
            }
            if (!String.IsNullOrEmpty(pathAddImg))
            {
                category.Image = pathAddImg;
            }
            category.DateCreated = DateTime.Now;
            category.IsDelete = false;

            categoryService.Create(category);

            imgBackgrAdd.Background = Brushes.Transparent;
            pathAddImg = null;
            editPhotoAdd.Visibility = Visibility.Hidden;
            delPhotoAdd.Visibility = Visibility.Hidden;
            nameAdd.Text = "";

            CategoriesVM vm = new CategoriesVM();
            comboboxUpd.ItemsSource = vm.GetCategories;
            comboboxDel.ItemsSource = vm.GetCategories;
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var category = comboboxDel.SelectedItem as CategoryDTO;
            categoryService.Delete(category.Id);

            CategoriesVM vm = new CategoriesVM();
            comboboxDel.ItemsSource = vm.GetCategories;
            comboboxUpd.ItemsSource = vm.GetCategories;

            comboboxUpd.SelectedIndex = -1;

        }
    }
}

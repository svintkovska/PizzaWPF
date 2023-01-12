using BLL.ModelsDTO;
using BLL.Services;
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
    /// Interaction logic for CategoryWindow.xaml
    /// </summary>
    public partial class CategoryWindow : Window
    {
        private CategoryService categoryService;
        private string base64UpdImg;
        private string base64AddImg;
        
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
                string pathUpdImg = fileDialog.FileName;

                byte[] imageBytes = File.ReadAllBytes(pathUpdImg);

                base64UpdImg = Convert.ToBase64String(imageBytes);

                base64UpdImg = UploadImagesToServer.UploadImage(base64UpdImg);
                imgBackgrUpd.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), base64UpdImg)));
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
                string pathUpdImg = fileDialog.FileName;

                byte[] imageBytes = File.ReadAllBytes(pathUpdImg);

                base64UpdImg = Convert.ToBase64String(imageBytes);

                base64UpdImg = UploadImagesToServer.UploadImage(base64UpdImg);
                imgBackgrUpd.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), base64UpdImg)));
            }
        }

        private void delPhotoUpd_Click(object sender, RoutedEventArgs e)
        {
            imgBackgrUpd.Background = Brushes.Transparent;
            base64UpdImg = null;
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
            if (!String.IsNullOrEmpty(base64UpdImg))
            {
                category.Image = base64UpdImg;
            }
            categoryService.Update(category.Id, category);

            imgBackgrUpd.Background = Brushes.Transparent;
            base64UpdImg = null;
            editPhotoUpd.Visibility = Visibility.Hidden;
            delPhotoUpd.Visibility = Visibility.Hidden;
            nameUpd.Text = "";

            comboboxUpd.SelectedIndex = -1;

        }

        private void selectImgAddBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
            fileDialog.Multiselect = false;
            if (fileDialog.ShowDialog() != false)
            {
                string pathAddImg = fileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(pathAddImg);

                base64AddImg = Convert.ToBase64String(imageBytes);

                base64AddImg = UploadImagesToServer.UploadImage(base64AddImg);
                imgBackgrAdd.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), base64AddImg)));
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
                string pathAddImg = fileDialog.FileName;
                byte[] imageBytes = File.ReadAllBytes(pathAddImg);

                base64AddImg = Convert.ToBase64String(imageBytes);

                base64AddImg = UploadImagesToServer.UploadImage(base64AddImg);
                imgBackgrAdd.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), base64AddImg)));
            }
        }

        private void delPhotoAdd_Click(object sender, RoutedEventArgs e)
        {
            imgBackgrAdd.Background = Brushes.Transparent;
            base64AddImg = null;
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
            if (!String.IsNullOrEmpty(base64AddImg))
            {
                category.Image = base64AddImg;
            }
            category.DateCreated = DateTime.Now;
            category.IsDelete = false;

            categoryService.Create(category);

            imgBackgrAdd.Background = Brushes.Transparent;
            base64AddImg = null;
            editPhotoAdd.Visibility = Visibility.Hidden;
            delPhotoAdd.Visibility = Visibility.Hidden;
            nameAdd.Text = "";

            var cm = DataContext as CategoriesVM;
            cm.GetCategories.Add(category);
        }

        private void deleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var category = comboboxDel.SelectedItem as CategoryDTO;
            categoryService.Delete(category.Id);

            var cm = DataContext as CategoriesVM;
            cm.GetCategories.Remove(category);
            comboboxUpd.SelectedIndex = -1;
        }

        private void Grid_MouseMove(object sender, MouseEventArgs e)
        {
            ValidateCreate();
            ValidateUpdate();
            ValidateDelete();
        }

        private void ValidateCreate()
        {
            if(String.IsNullOrEmpty(nameAdd.Text))
                addBtn.IsEnabled = false;
            else if(String.IsNullOrEmpty(base64AddImg))
                addBtn.IsEnabled = false;
            else
                addBtn.IsEnabled = true;
        }

        private void ValidateUpdate()
        {
            var category = comboboxUpd.SelectedItem as CategoryDTO;

            if(category == null)
                updateBtn.IsEnabled = false;
            else
                updateBtn.IsEnabled = true;
        }

        private void ValidateDelete()
        {
            var category = comboboxDel.SelectedItem as CategoryDTO;
            if (category == null)
                deleteBtn.IsEnabled = false;
            else
                deleteBtn.IsEnabled = true;

        }
    }
}

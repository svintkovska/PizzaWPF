using GalaSoft.MvvmLight.Command;
using PizzaUI.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Controls;
using System.Windows.Input;

namespace PizzaUI.ViewModels
{
    public class PageVM : INotifyPropertyChanged
    {
        private Page categoriesPage;
        private Page adminPage;
        public Page currentPage;
        public Page CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
        public PageVM()
        {
            CreatePages();
        }
        private void CreatePages()
        {
            categoriesPage = new CategoriesPage();
            adminPage = new AdminPage();
            CurrentPage = adminPage;
        }

        public ICommand MainBtnClick
        {
            get
            {
                return new RelayCommand(() => CurrentPage = categoriesPage);
            }
        }
        //public ICommand LLibraryBtnClick
        //{
        //    get
        //    {
        //        return new RelayCommand(() => CurrentPage = libraryPage);
        //    }
        //}
        //public ICommand AddBookBtnClick
        //{
        //    get
        //    {
        //        return new RelayCommand(() => CurrentPage = addBookPage);
        //    }
        //}
    }
}

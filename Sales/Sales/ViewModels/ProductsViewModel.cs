namespace Sales.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Services;
    using Xamarin.Forms;

    public class ProductsViewModel:BaseViewModel {

        private ApiService apiService;

        private ObservableCollection<Product> products;

        private bool isRefreshing;

        public ObservableCollection<Product> ProductsList
        {
            get { return products; }
            set { SetValue(ref products, value); }
        }

        public bool IsRefreshing
        {
            get { return isRefreshing; }
            set { SetValue(ref isRefreshing, value); }
        }

        public ProductsViewModel()
        {
            apiService = new ApiService();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            IsRefreshing = true;
            Response connection = await apiService.CheckConnection();
            if (!connection.IsSuccess) {
                IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Accept");
                return;
            }
            string url = Application.Current.Resources["UrlAPI"].ToString();
            Response response = await apiService.GetList<Product>(url, "/api", "/Products");
            if (!response.IsSuccess) {
                IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            ProductsList = new ObservableCollection<Product>((List<Product>)response.Result);
            IsRefreshing = false;
        }

        public ICommand RefreshCommand
        {
            get { return new RelayCommand(LoadProducts); }
        }
    }
}

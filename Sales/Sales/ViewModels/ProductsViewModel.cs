namespace Sales.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Common.Models;
    using GalaSoft.MvvmLight.Command;
    using Sales.Helpers;
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
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }
            string url = Application.Current.Resources["UrlAPI"].ToString();
            string prefix = Application.Current.Resources["UrlPrefix"].ToString();
            string controller = Application.Current.Resources["UrlProductsController"].ToString();
            Response response = await apiService.GetList<Product>(url, prefix, controller);
            if (!response.IsSuccess) {
                IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
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

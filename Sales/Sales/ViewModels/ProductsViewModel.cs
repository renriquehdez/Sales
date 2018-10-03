namespace Sales.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Sales.Common.Models;
    using Sales.Helpers;
    using Services;
    using Xamarin.Forms;

    public class ProductsViewModel : BaseViewModel
    {
        #region Atributtes
        private ApiService apiService;
        private ObservableCollection<Product> products;
        private bool isRefreshing;
        #endregion

        #region Properties
        public ObservableCollection<Product> Products
        {
            get { return this.products; }
            set{ this.SetValue(ref this.products, value);}
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }
        #endregion

        #region Contructors
        public ProductsViewModel()
        {
            this.apiService = new ApiService();
            this.LoadProducts();
        }
        #endregion

        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;

                await Application.Current.MainPage.DisplayAlert(
                   Languages.Error,
                   connection.Message,
                   Languages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();

            //var url = "https://f0990b3e.ngrok.io";
            var response = await this.apiService.GetList<Product>(
                url,
                prefix,
                controller);
   
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;

                await Application.Current.MainPage.DisplayAlert(
                   Languages.Error,
                   connection.Message,
                   Languages.Accept);
                return;
            }

            var list = (List<Product>)response.Result;
            this.Products = new ObservableCollection<Product>(list);

            this.IsRefreshing = false;
        }

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
               return new RelayCommand(LoadProducts);
            }
        }
        #endregion
    }
}

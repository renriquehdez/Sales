namespace Sales.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Sales.Common.Models;
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
                   "Error",
                   connection.Message,
                   "Accept");
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            //var url = "https://f0990b3e.ngrok.io";
            var response = await this.apiService.GetList<Product>(
                url,
                "/salesapi/api",
                "/products");
   
            if (!response.IsSuccess)
            {
                this.IsRefreshing = false;

                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Accept");
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

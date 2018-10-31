namespace Sales.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using GalaSoft.MvvmLight.Command;
    using Common.Models;
    using Helpers;
    using Services;
    using Xamarin.Forms;
    using System.Linq;
    using System.Threading.Tasks;

    public class ProductsViewModel : BaseViewModel
    {
        #region Atributtes
        private ApiService apiService;
        private DataService dataService;
        private bool isRefreshing;
        private ObservableCollection<ProductItemViewModel> products;
        private string filter;
        private Category category;
        #endregion

        #region Properties
        public ObservableCollection<ProductItemViewModel> Products
        {
            get
            {
                return this.products;
            }
            set
            {
                this.SetValue(ref this.products, value);
            }
        }

        public bool IsRefreshing
        {
            get
            {
                return this.isRefreshing;
            }
            set
            {
                this.SetValue(ref this.isRefreshing, value);
            }
        }

        public List<Product> MyProducts
        {
            get; set;
        }

        public string Filter
        {
            get
            {
                return this.filter;
            }
            set
            {
                this.SetValue(ref this.filter, value);
                this.RefreshList();
            }
        }

        public Category Category
        {
            get { return this.category; }
            set { this.SetValue(ref this.category, value); }
        }
        #endregion

        #region Contructors
        public ProductsViewModel(Category category)
        {
            instance = this;
            this.Category = category;
            this.apiService = new ApiService();
            this.dataService = new DataService();
            this.LoadProducts();
        }
        #endregion

        #region Singleton
        private static ProductsViewModel instance;
        

        public static ProductsViewModel GetInstance()
        {
            return instance;
        }
        #endregion

        #region Commands
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProducts);
            }
        }

        //private async void LoadProducts()
        //{
        //    this.IsRefreshing = true;
        //    var connection = await this.apiService.CheckConnection();
        //    if (connection.IsSuccess)
        //    {
        //        var answer = await this.LoadProductsFromAPI();
        //        if (answer)
        //        {
        //            this.SaveProductsToDB();
        //        }
        //    }
        //    else
        //    {
        //        await this.LoadProductsFromDB();
        //    }
        //    if (this.MyProducts == null || this.MyProducts.Count == 0)
        //    {
        //        this.IsRefreshing = false;
        //        await Application.Current.MainPage.DisplayAlert(Languages.Error, Languages.NoProductsMessage, Languages.Accept);
        //        return;
        //    }

        //    this.RefreshList();
        //    this.IsRefreshing = false;
        //}

        private async void LoadProducts()
        {
            this.IsRefreshing = true;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            var answer = await this.LoadProductsFromAPI();
            if (answer)
            {
                this.RefreshList();
            }

            this.IsRefreshing = false;
        }



        private async Task LoadProductsFromDB()
        {
            this.MyProducts = await this.dataService.GetAllProducts();
        }

        private async Task SaveProductsToDB()
        {
            await this.dataService.DeleteAllProducts();
            this.dataService.Insert(this.MyProducts);
        }

        //private async Task<bool> LoadProductsFromAPI()
        //{
        //    var url = Application.Current.Resources["UrlAPI"].ToString();
        //    var prefix = Application.Current.Resources["UrlPrefix"].ToString();
        //    var controller = Application.Current.Resources["UrlProductsController"].ToString();
        //    var response = await this.apiService.GetList<Product>(url, prefix, controller, Settings.TokenType, Settings.AccessToken);

        //    if (!response.IsSuccess)
        //    {
        //        this.IsRefreshing = false;
        //        return false;
        //    }

        //    this.MyProducts = (List<Product>)response.Result;
        //    return true;
        //}
        private async Task<bool> LoadProductsFromAPI()
        {
            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();
            var response = await this.apiService.GetList<Product>(url, prefix, controller, this.Category.CategoryId, Settings.TokenType, Settings.AccessToken);
            if (!response.IsSuccess)
            {
                return false;
            }

            this.MyProducts = (List<Product>)response.Result;
            return true;
        }


        public void RefreshList()
        {
            if (string.IsNullOrEmpty(this.Filter))
            {
                var myListProductItemViewModel = this.MyProducts.Select(p => new ProductItemViewModel
                {
                    Description = p.Description,
                    ImageArray = p.ImageArray,
                    ImagePath = p.ImagePath,
                    IsAvailable = p.IsAvailable,
                    Price = p.Price,
                    ProductID = p.ProductID,
                    PublishOn = p.PublishOn,
                    Remarks = p.Remarks,
                });

                this.Products = new ObservableCollection<ProductItemViewModel>(
                    myListProductItemViewModel.OrderBy(p => p.Description));
            }
            else
            {
                var myListProductItemViewModel = this.MyProducts.Select(p => new ProductItemViewModel
                {
                    Description = p.Description,
                    ImageArray = p.ImageArray,
                    ImagePath = p.ImagePath,
                    IsAvailable = p.IsAvailable,
                    Price = p.Price,
                    ProductID = p.ProductID,
                    PublishOn = p.PublishOn,
                    Remarks = p.Remarks,
                }).Where(p => p.Description.ToLower().Contains(this.Filter.ToLower()));

                this.Products = new ObservableCollection<ProductItemViewModel>(
                    myListProductItemViewModel.OrderBy(p => p.Description));
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(RefreshList);
            }
        }
        #endregion
    }
}

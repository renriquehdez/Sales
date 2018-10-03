namespace Sales.ViewModels
{
    public class MainViewModel
    {
        #region Properties
        public ProductsViewModel Products
        {
            get; set;
        }
        #endregion

        #region Contructors
        public MainViewModel()
        {
            this.Products = new ProductsViewModel();
        }
        #endregion
    }
}

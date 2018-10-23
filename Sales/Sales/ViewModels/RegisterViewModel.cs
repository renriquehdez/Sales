
namespace Sales.ViewModels
{
    using Plugin.Media.Abstractions;
    using Services;
    using Xamarin.Forms;

    public class RegisterViewModel : BaseViewModel
    {
        #region Attributes
        private MediaFile file;
        private ImageSource imageSource;
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region Properties
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EMail { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Password { get; set; }

        public string PasswordConfirm { get; set; }

        public bool IsRunning
        {
            get
            {
                return this.isRunning;
            }
            set
            {
                this.SetValue(ref this.isRunning, value);
            }
        }

        public bool IsEnabled
        {
            get
            {
                return this.isEnabled;
            }
            set
            {
                this.SetValue(ref this.isEnabled, value);
            }
        }

        public ImageSource ImageSource
        {
            get
            {
                return this.imageSource;
            }
            set
            {
                this.SetValue(ref this.imageSource, value);
            }
        }
        #endregion

        #region Constructors
        public RegisterViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.ImageSource = "if_user";
        }
        #endregion

        #region Commands

        #endregion
    }
}

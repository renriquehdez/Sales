namespace Sales.Common.Models
{
    public class Claim
    {
        #region Properties
        public int Id
        {
            get; set;
        }

        public string UserId
        {
            get; set;
        }

        public string ClaimType
        {
            get; set;
        }

        public string ClaimValue
        {
            get; set;
        } 
        #endregion
    }

}
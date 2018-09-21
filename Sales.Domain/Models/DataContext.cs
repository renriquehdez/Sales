

namespace Sales.Domain.Models
{
    using System.Data.Entity;
    public class DataContext : DbContext
    {
        #region Constructors
        public DataContext() : base("DefaultConnection")
        {
        }
        #endregion

        public System.Data.Entity.DbSet<Sales.Common.Models.Product> Products
        {
            get; set;
        }
    }
}

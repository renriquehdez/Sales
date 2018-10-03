

namespace Sales.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    public class Product
    {
        #region Properties
        [Key]
        public int ProductID
        {
            get; set;
        }

        [Required]
        public string Description
        {
            get; set;
        }

        public Decimal Price
        {
            get; set;
        }

        public bool IsAvailable
        {
            get; set;
        }

        public DateTime PublishOn
        {
            get; set;
        }
        #endregion

        #region Methods
        public override string ToString()
        {
            return this.Description;
        }
        #endregion
    }
}

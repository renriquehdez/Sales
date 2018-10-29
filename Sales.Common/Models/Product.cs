

namespace Sales.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        #region Properties
        [Key]
        public int ProductID { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string Description { get; set; }

        [DataType(DataType.MultilineText)]
        public string Remarks { get; set; }

        [Display(Name ="Image")]
        public string ImagePath{ get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        public Decimal Price { get; set; }

        [Display(Name = "Is Available")]
        public bool IsAvailable { get; set; }

        [Display(Name = "Publish On")]
        [DataType(DataType.Date)]
        public DateTime PublishOn { get; set; }

        [Required]
        [StringLength(128)]
        public string UserId { get; set; }

        public virtual Category Category { get; set; }

        [NotMapped]
        public byte[] ImageArray { get; set; }

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "noproduct";
                }

                return $"https://b205c84d.ngrok.io/salesapi/{this.ImagePath.Substring(1)}";
            }
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

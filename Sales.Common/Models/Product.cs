﻿

namespace Sales.Common.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Product
    {
        #region Properties
        [Key]
        public int ProductID { get; set; }

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

        public string ImageFullPath
        {
            get
            {
                if (string.IsNullOrEmpty(this.ImagePath))
                {
                    return "noproduct";
                }

                return $"https://f0990b3e.ngrok.io/salesbackend/{this.ImagePath.Substring(1)}";
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

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BeStreet.Models
{
    public partial class Product
    {
        [Key]
        [Required(ErrorMessage = "Required Product ID")]
        [Display(Name = "Product ID")]
        public string PdId { get; set; } = null;

        [Required(ErrorMessage = "Required Color")]
        [Display(Name = "Color")]
        public byte ColorId { get; set; }

        [Required(ErrorMessage = "Required Size")]
        [Display(Name = "Size")]
        public byte SizeId { get; set; }

        [Required(ErrorMessage = "Required Product Name")]
        [Display(Name = "Product Name")]
        public string PdName { get; set; } = null;

        [Display(Name = "Product Detail")]
        public string PdDtls { get; set; }

        [Required(ErrorMessage = "Required Price")]
        [Display(Name = "Price")]
        public double? PdPrice { get; set; }

        [Required(ErrorMessage = "Required Cost")]
        [Display(Name = "Cost")]
        public double? PdCost { get; set; }

        [Display(Name = "Inventories")]
        public int? PdStk { get; set; }

        [Display(Name = "Type")]
        public byte? PdtId { get; set; }

        [Display(Name = "Status")]
        public byte? StatusId { get; set; }

        [Display(Name = "Target")]
        public byte? TargetId { get; set; }

        [Display(Name = "Suppliers")]
        public string SupId { get; set; }

        public DateTime PdLastBuy { get; set; }

        public DateTime PdLastSale { get; set; }
    }
}

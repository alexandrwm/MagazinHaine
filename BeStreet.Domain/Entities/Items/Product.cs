using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeStreet.Domain.Entities.Items
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Product ID")]
        public int PdId { get; set; }

        [Required(ErrorMessage = "Required Color")]
        [Display(Name = "Color")]
        public int ColorId { get; set; }

        [Required(ErrorMessage = "Required Size")]
        [Display(Name = "Size")]
        public int SizeId { get; set; }

        [Required(ErrorMessage = "Required Product Name")]
        [Display(Name = "Product Name")]
        public string PdName { get; set; }

        [Display(Name = "Product Detail")]
        public string PdDtls { get; set; }

        [Required(ErrorMessage = "Required Price")]
        [Display(Name = "Price")]
        public double PdPrice { get; set; }

        //[Required(ErrorMessage = "Required Cost")]
        //[Display(Name = "Cost")]
        //public double PdCost { get; set; }

        [Display(Name = "Inventories")]
        public int PdStk { get; set; }

        [Display(Name = "Type")]
        public int PdtId { get; set; }

        [Display(Name = "Status")]
        public int StatusId { get; set; }

        [Display(Name = "Target")]
        public int TargetId { get; set; }

        [Display(Name = "Suppliers")]
        public string SupId { get; set; }

        public DateTime PdLastBuy { get; set; }

        public DateTime PdLastSale { get; set; }
    }
}

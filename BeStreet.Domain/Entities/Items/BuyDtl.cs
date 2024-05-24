
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeStreet.Models
{

    public class BuyDtl
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Buying ID")]
        public int BuyId { get; set; }

        [Display(Name = "Product")]
        public string PdId { get; set; } = null;
        [Display(Name = "Quantity")]
        public int? BdtlQty { get; set; }

        [Display(Name = "Price / piece")]
        public double? BdtlPrice { get; set; }

        [Display(Name = "Total Price")]
        public double? BdtlMoney { get; set; }
    }
}
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeStreet.Models
{
    // HAS FOREIGN KEY CUSID
    public class Cart
    {
        // public string CartId { get; set; } = null;

        [Key, ForeignKey("Customer")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CusId { get; set; }

        public DateTime CartDate { get; set; }

        public double? CartMoney { get; set; }

        public int? CartQty { get; set; }

        [StringLength(1)]
        [DefaultValue("N")]
        public string CartCf { get; set; }

        [StringLength(1)]
        [DefaultValue("N")]
        public string CartPay { get; set; }

        [StringLength(1)]
        [DefaultValue("N")]
        public string CartSend { get; set; }

        [StringLength(1)]
        [DefaultValue("N")]
        public string CartVoid { get; set; }
    }
}


using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeStreet.Domain.Entities.Items
{
    public class Cart
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CartId { get; set; }

        [Required]
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


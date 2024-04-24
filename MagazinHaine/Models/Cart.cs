using System;

namespace MagazinHaine.Models
{


    public partial class Cart
    {
        public string CartId { get; set; } = null;

        public string CusId { get; set; }

        public DateTime CartDate { get; set; }

        public double? CartMoney { get; set; }

        public int? CartQty { get; set; }

        public string CartCf { get; set; }

        public string CartPay { get; set; }

        public string CartSend { get; set; }

        public string CartVoid { get; set; }
    }
}


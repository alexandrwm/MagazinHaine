using System;

namespace BeStreet.Domain.Entities.Items
{
    public partial class Buying
    {
        public string BuyId { get; set; } = null;

        public string SupId { get; set; }

        public DateTime BuyDate { get; set; }

        public string StfId { get; set; }

        public string BuyDocId { get; set; }

        public string Saleman { get; set; }

        public int? BuyQty { get; set; }

        public double? BuyMoney { get; set; }

        public string BuyRemark { get; set; }
    }
}

﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BeStreet.Domain.Entities.Items
{
    // HAS FOREIGN KEY CUSID
    public class Cart
    {
        public string CartId { get; set; } = null;

        public string CusId { get; set; }

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


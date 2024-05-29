using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.Domain.Entities.Carts
{
    public class AddItemResp
    {
        public bool Status { get; set; }
        public int CartQty { get; set; }
        public double CartMoney { get; set; }
    }
}

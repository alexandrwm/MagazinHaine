using BeStreet.Domain.Entities.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.Domain.Entities.Carts
{
    public class GetCartResp
    {
        public bool AlreadyExists { get; set; }
        public Cart Cart { get; set; }
    }
}

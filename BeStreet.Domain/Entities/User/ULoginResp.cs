using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.Domain.Entities.User
{
    public class ULoginResp
    {
        public bool Status {  get; set; }
        public int? CusId { get; set; }
        public string CusName { get; set; }
    }
}

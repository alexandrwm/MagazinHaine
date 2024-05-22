using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.Domain.Entities.User
{
    public class URegData
    {
        public string CusName { get; set; }
        public string CusLogin { get; set; }
        public string CusPass { get; set; }
        public string CusEmail { get; set; }
        public string CusAdd { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastLogin { get; set; }

    }
}

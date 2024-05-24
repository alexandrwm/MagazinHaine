using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.Domain.Entities.User
{
    public class ULoginData
    {
        public string Login { get; set; }
        public string Pass { get; set; }
        public DateTime  LastLogin {  get; set; }

    }
}

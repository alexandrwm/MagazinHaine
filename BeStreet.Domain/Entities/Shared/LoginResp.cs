using BeStreet.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.Domain.Entities.Shared
{
    public class LoginResp
    {
        public bool Status {  get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public URole Role { get; set; }
    }
}

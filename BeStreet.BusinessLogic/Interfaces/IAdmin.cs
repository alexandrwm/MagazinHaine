using BeStreet.Domain.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.BusinessLogic.Interfaces
{
    public interface IAdmin
    {
        LoginResp AdminLogin(string userName, string userPass);
    }
}

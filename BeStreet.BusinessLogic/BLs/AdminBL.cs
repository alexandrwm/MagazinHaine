using BeStreet.BusinessLogic.Core;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.BusinessLogic.BLs
{
    public class AdminBL : AdminApi, IAdmin
    {
        public LoginResp AdminLogin(string userName, string userPass)
        {
            throw new NotImplementedException();
        }
    }
}

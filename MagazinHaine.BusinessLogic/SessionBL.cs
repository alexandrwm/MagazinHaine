using BeStreet.BusinessLogic.Core;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public bool UserLogin(ULoginData data)
        {
            return UserLoginAction(data);
        }

        public bool UserReg(URegData data)
        {
            return UserRegAction(data);
        }
    }
}

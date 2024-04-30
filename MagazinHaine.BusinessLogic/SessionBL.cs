using MagazinHaine.BusinessLogic.Core;
using MagazinHaine.BusinessLogic.Interfaces;
using MagazinHaine.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagazinHaine.BusinessLogic
{
    public class SessionBL : UserApi, ISession
    {
        public bool UserLogin(ULoginData data)
        {
            return UserLoginAction(data);
        }
    }
}

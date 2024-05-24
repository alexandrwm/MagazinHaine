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
        public ULoginResp UserLogin(ULoginData data)
        {
            return UserLoginAction(data);
        }

        public UserProfile GetUserProf(int? id)
        {
            return GetUserProfileAction(id);
        }

        public bool UserReg(URegData data)
        {
            return UserRegAction(data);
        }

        public int? UserUpdateByUsername(UserProfile profile, string cusLogin)
        {
            return UserUpdatebyUsernameAction(profile, cusLogin);
        }
    }
}

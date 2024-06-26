﻿using BeStreet.BusinessLogic.Core;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.Shared;
using BeStreet.Domain.Entities.User;
using System.Web;

namespace BeStreet.BusinessLogic.BLs
{
    public class SessionBL : UserApi, ISession
    {
        public LoginResp UserLogin(ULoginData data)
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

        public UserMinimal GetUserByCookie(string userCookie)
        {
            return GetUserByCookieAction(userCookie);
        }

        public HttpCookie GetCookie(string data)
        {
            return GetCookieAction(data);
        }
    }
}

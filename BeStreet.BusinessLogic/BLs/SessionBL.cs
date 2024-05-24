using BeStreet.BusinessLogic.Core;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.Shared;
using BeStreet.Domain.Entities.User;

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
    }
}

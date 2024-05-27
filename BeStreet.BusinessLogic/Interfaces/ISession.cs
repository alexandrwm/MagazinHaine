using BeStreet.Domain.Entities.Shared;
using BeStreet.Domain.Entities.User;
using System.Web;

namespace BeStreet.BusinessLogic.Interfaces
{
    public interface ISession
    {
        LoginResp UserLogin(ULoginData data);
        bool UserReg(URegData data);
        UserProfile GetUserProf(int? id);
        int? UserUpdateByUsername(UserProfile profile, string cusLogin);
        UserMinimal GetUserByCookie(string value);
        HttpCookie GetCookie(string data);
    }
}

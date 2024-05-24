using BeStreet.Domain.Entities.Shared;
using BeStreet.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.BusinessLogic.Interfaces
{
    public interface ISession
    {
        LoginResp UserLogin(ULoginData data);
        bool UserReg(URegData data);
        UserProfile GetUserProf(int? id);
        int? UserUpdateByUsername(UserProfile profile, string cusLogin);
    }
}

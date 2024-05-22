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
        bool UserLogin(ULoginData data);
        bool UserReg(URegData data);
    }
}

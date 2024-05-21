using BeStreet.Domain.Entities.User;
using BeStreet.BusinessLogic.DbContexts;
using BeStreet.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.BusinessLogic.Core
{
    public class UserApi
    {
        public bool UserLoginAction(ULoginData data)
        {
            UDbTable user;
            using (var db = new UserContext())
            {
                user = db.Users.FirstOrDefault(u => u.Username == data.Username && u.Password == data.Password);
            }
            return user != null;
        }
    }
}

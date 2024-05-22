using BeStreet.BusinessLogic.DbContexts;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.User;
using System.Data.Entity;
using System.Linq;

namespace BeStreet.BusinessLogic.Core
{
    public class UserApi
    {
        public bool UserLoginAction(ULoginData data)
        {
            Customer user;
            using (var db = new BeStreetContext())
            {
                user = db.Customers.FirstOrDefault(u => u.CusLogin == data.CusLogin && u.CusPass == data.CusPass);
                if (user != null)
                {
                    user.LastLogin = data.LastLogin;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            return user != null;
        }

        public bool UserRegAction(URegData data)
        {
            Customer user;
            using (var db = new BeStreetContext())
            {
                user = db.Customers.FirstOrDefault(u => u.CusLogin == data.CusLogin);
                if (user != null) return false;

                db.Customers.Add(new Customer
                {
                    CusName = data.CusName,
                    CusLogin = data.CusLogin,
                    CusPass = data.CusPass,
                    CusEmail = data.CusEmail,
                    CusAdd = data.CusAdd,
                    StartDate = data.StartDate,
                    LastLogin = data.LastLogin
                });
                db.SaveChanges();
            }
            return true;
        }
    }
}

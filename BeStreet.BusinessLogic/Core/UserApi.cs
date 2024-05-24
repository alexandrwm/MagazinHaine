using BeStreet.BusinessLogic.DbContexts;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.User;
using System.Data.Entity;
using System.Linq;

namespace BeStreet.BusinessLogic.Core
{
    public class UserApi
    {
        public ULoginResp UserLoginAction(ULoginData data)
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
            return new ULoginResp { Status = user != null, CusId = user?.CusId, CusName = user?.CusName };
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

        public UserProfile GetUserProfileAction(int? id)
        {
            if (id == null) return null;
            UserProfile profile = new UserProfile();
            using (var db = new BeStreetContext())
            {
                var user = db.Customers.Find(id);
                if (user != null)
                {
                    profile.CusName = user.CusName;
                    profile.CusLogin = user.CusLogin;
                    // don't get the password obviously
                    //profile.CusPass = user.CusPass;
                    profile.CusEmail = user.CusEmail;
                    profile.CusAdd = user.CusAdd;
                    return profile;
                }
            }
            return null;
        }

        public int? UserUpdatebyUsernameAction(UserProfile profile, string cusLogin)
        {
            int? cusId = null;
            using (var db = new BeStreetContext())
            {
                var user  = db.Customers.FirstOrDefault(u => u.CusLogin == cusLogin && u.CusPass == profile.CusPass);
                if (user != null)
                {
                    user.CusName = profile.CusName;
                    user.CusEmail = profile.CusEmail;
                    user.CusAdd = profile.CusAdd;
                }
                cusId = user.CusId;
                db.SaveChanges();
            }
            return cusId;
        }
    }
}

using BeStreet.BusinessLogic.DbContexts;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.Shared;
using BeStreet.Domain.Entities.User;
using BeStreet.Domain.Entities.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
//
namespace BeStreet.BusinessLogic.Core
{
    public class UserApi
    {
        public LoginResp UserLoginAction(ULoginData data)
        {
            User user;
            using (var db = new BeStreetContext())
            {
                user = db.Users.FirstOrDefault(u => u.Login == data.Login && u.Pass == data.Pass);
                if (user != null)
                {
                    user.LastLogin = data.LastLogin;
                    db.Entry(user).State = EntityState.Modified;
                    db.SaveChanges();
                }
                else
                {
                    return new LoginResp { Status = false };
                }
            }
            return new LoginResp { Status = user != null, Id = user.Id, Name = user.Name, Role = user.Role };
        }

        public bool UserRegAction(URegData data)
        {
            User user;
            using (var db = new BeStreetContext())
            {
                user = db.Users.FirstOrDefault(u => u.Login == data.Login);
                if (user != null) return false;

                db.Users.Add(new User
                {
                    Name = data.Name,
                    Login = data.Login,
                    Pass = data.Pass,
                    Email = data.Email,
                    Add = data.Add,
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
                var user = db.Users.Find(id);
                if (user != null)
                {
                    profile.Name = user.Name;
                    profile.Login = user.Login;
                    // don't get the password obviously
                    //profile.Pass = user.Pass;
                    profile.Email = user.Email;
                    profile.Add = user.Add;
                    return profile;
                }
            }
            return null;
        }

        public int? UserUpdatebyUsernameAction(UserProfile profile, string login)
        {
            int? cusId = null;
            using (var db = new BeStreetContext())
            {
                var user  = db.Users.FirstOrDefault(u => u.Login == login && u.Pass == profile.Pass);
                if (user != null)
                {
                    user.Name = profile.Name;
                    user.Email = profile.Email;
                    user.Add = profile.Add;
                    cusId = user.Id;
                }
                db.SaveChanges();
            }
            return cusId;
        }

    }
}

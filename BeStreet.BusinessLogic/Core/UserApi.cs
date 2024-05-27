using AutoMapper;
using BeStreet.BusinessLogic.DbContexts;
using BeStreet.Domain.Entities.Shared;
using BeStreet.Domain.Entities.User;
using BeStreet.Helpers;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BeStreet.BusinessLogic.Core
{
    public class UserApi
    {
        public LoginResp UserLoginAction(ULoginData data)
        {
            Customer user;
            using (var db = new BeStreetContext())
            {
                user = db.Customers.FirstOrDefault(u => u.Login == data.Login && u.Pass == data.Pass);
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
            Customer user;
            using (var db = new BeStreetContext())
            {
                user = db.Customers.FirstOrDefault(u => u.Login == data.Login);
                if (user != null) return false;

                db.Customers.Add(new Customer
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
                var user = db.Customers.Find(id);
                if (user != null)
                {
                    profile.Name = user.Name;
                    profile.Login = user.Login;
                    // don't get the password obviously
                    //profile.Pass = user.Pass;
                    profile.Email = user.Email;
                    profile.Add = user.Add;
                    profile.Id = user.Id;
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
                var user = db.Customers.FirstOrDefault(u => u.Login == login && u.Pass == profile.Pass);
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

        internal UserMinimal GetUserByCookieAction(string userCookie)
        {
            Session session;
            using (var db = new SessionContext())
            {
                session = db.Sessions.FirstOrDefault(s => s.ExpireTime > DateTime.Now && s.CookieString == userCookie);
                if (session != null)
                {
                    bool BUG = session.ExpireTime > DateTime.Now;
                    if (!BUG) return null;
                }
            }
            if (session == null) return null;

            using (var db = new BeStreetContext())
            {
                var user = db.Customers.FirstOrDefault(u => u.Login == session.Login);
                if (user == null) return null;

                UserMinimal minimal;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<Customer, UserMinimal>());
                minimal = config.CreateMapper().Map<UserMinimal>(user);

                return minimal;
            }
        }

        internal HttpCookie GetCookieAction(string data)
        {
            var cookie = new HttpCookie("UserCookie")
            {
                Value = CookieGenerator.Create(data),
            };

            using (var debil = new SessionContext())
            {
                Session current;
                current = debil.Sessions.FirstOrDefault(s => s.Login == data);
                int sessionLength = 10;

                if (current != null)
                {
                    current.CookieString = cookie.Value;
                    current.ExpireTime = DateTime.Now.AddMinutes(sessionLength);
                }
                else
                {
                    debil.Sessions.Add(new Session()
                    {
                        Login = data,
                        CookieString = cookie.Value,
                        ExpireTime = DateTime.Now.AddMinutes(sessionLength)
                    });
                }
                debil.SaveChanges();
            }

            return cookie;
        }
    }
}

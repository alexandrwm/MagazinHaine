using BeStreet.BusinessLogic.DbContexts;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.Shared;
using BeStreet.Domain.Entities.User;
using BeStreet.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

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
                }
                cusId = user.Id;
                db.SaveChanges();
            }
            return cusId;
        }

        //public  LoadMenProductsAction()
        //{
        //    using (var db = new BeStreetContext())
        //    {
        //        var pdvm = from p in db.Products

        //                   join pt in db.ProductTypes on p.PdtId equals pt.PdtId into join_p_pt

        //                   from p_pt in join_p_pt.DefaultIfEmpty()

        //                   join color in db.Colors on p.ColorId equals color.ColorId into join_p_color
        //                   from p_color in join_p_color.DefaultIfEmpty()

        //                   join size in db.Sizes on p.SizeId equals size.SizeId into join_p_size
        //                   from p_size in join_p_size.DefaultIfEmpty()

        //                   join target in db.Targets on p.TargetId equals target.TargetId into join_p_target
        //                   from p_target in join_p_target.DefaultIfEmpty()

        //                   join status in db.Statuses on p.StatusId equals status.StatusId into join_p_status
        //                   from p_status in join_p_status.DefaultIfEmpty()

        //                   where p_target.TargetName.Equals("Barbati")

        //                   select new PdFilterVM
        //                   {
        //                       PdId = p.PdId,
        //                       ColorId = p.ColorId,
        //                       ColorName = p_color.ColorName,
        //                       SizeId = p_size.SizeId,
        //                       SizeName = p_size.SizeName,
        //                       TargetName = p_target.TargetName,
        //                       PdName = p.PdName,
        //                       PdtName = p_pt.PdtName,
        //                       PdPrice = p.PdPrice,
        //                       PdCost = p.PdCost,
        //                       PdStk = p.PdStk,
        //                       StatusName = p_status.StatusName,
        //                   };
        //        return pdvm;
        //    }
        //}
    }
}

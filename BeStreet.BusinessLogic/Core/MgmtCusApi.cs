using BeStreet.BusinessLogic.DbContexts;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.User;
using BeStreet.Domain.Entities.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BeStreet.BusinessLogic.Core
{
    public class MgmtCusApi
    {
        internal List<CustomerVM> GetAllCustomersAction()
        {
            using (var db = new BeStreetContext())
            {
                var CustomerVM = from c in db.Customers

                                 select new CustomerVM
                                 {
                                     Id = c.Id,
                                     Name = c.Name,
                                     Login = c.Login,
                                     Pass = c.Pass,
                                     Email = c.Email,
                                     Role = c.Role,
                                     Add = c.Add,
                                     StartDate = c.StartDate,
                                     LastLogin = c.LastLogin,
                                 };

                return CustomerVM.ToList();
            }
        }

        public bool CreateCustomerAction(Customer obj)
        {
            using (var db = new BeStreetContext())
            {
                var cus = db.Customers.FirstOrDefault(s => s.Login == obj.Login);
                if (cus != null) return false;

                db.Customers.Add(obj);
                db.SaveChanges();
            }
            return true;
        }

        public Customer GetCustomerByIdAction(int? id)
        {
            if (id == null) return null;
            Customer cus;
            using (var db = new BeStreetContext())
            {
                cus = db.Customers.FirstOrDefault(s => s.Id == id);
            }
            return cus;
        }

        internal bool UpdateCustomerAction(Customer obj)
        {
            using (var db = new BeStreetContext())
            {
                var cus = db.Customers.FirstOrDefault(s => s.Id == obj.Id);
                if (cus == null) return false;

                cus.Name = obj.Name;
                cus.Login = obj.Login;
                cus.Email = obj.Email;
                cus.Role = obj.Role;

                db.SaveChanges();
            }
            return true;
        }

        internal bool DeleteCustomerByIdAction(int id)
        {
            using (var db = new BeStreetContext())
            {
                var cus = db.Customers.FirstOrDefault(s => s.Id == id);
                if (cus == null) return false;

                db.Customers.Remove(cus);
                db.SaveChanges();
            }
            return true;
        }
    }
}

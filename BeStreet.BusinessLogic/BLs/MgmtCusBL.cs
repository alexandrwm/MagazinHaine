using BeStreet.BusinessLogic.Core;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.User;
using BeStreet.Domain.Entities.ViewModels;
using System.Collections.Generic;

namespace BeStreet.BusinessLogic.BLs
{
    public class MgmtCusBL : MgmtCusApi, IMgmtCus
    {
        public bool CreateCustomer(Customer obj)
        {
            return CreateCustomerAction(obj);
        }

        public bool DeleteCustomerById(int supId)
        {
            return DeleteCustomerByIdAction(supId);
        }

        public List<CustomerVM> GetAllCustomers()
        {
            return GetAllCustomersAction();
        }

        public Customer GetCustomerById(int? id)
        {
            return GetCustomerByIdAction(id);
        }

        public bool UpdateCustomer(Customer obj)
        {
            return UpdateCustomerAction(obj);
        }
    }
}

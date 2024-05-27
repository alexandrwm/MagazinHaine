using BeStreet.Domain.Entities.User;
using BeStreet.Domain.Entities.ViewModels;
using System.Collections.Generic;

namespace BeStreet.BusinessLogic.Interfaces
{
    public interface IMgmtCus
    {
        List<CustomerVM> GetAllCustomers();
        bool CreateCustomer(Customer obj);
        Customer GetCustomerById(int? id);
        bool UpdateCustomer(Customer obj);
        bool DeleteCustomerById(int id);
    }
}

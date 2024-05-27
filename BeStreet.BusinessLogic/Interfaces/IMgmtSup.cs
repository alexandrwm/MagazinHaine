using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.ViewModels;
using System.Collections.Generic;

namespace BeStreet.BusinessLogic.Interfaces
{
    public interface IMgmtSup
    {
        List<SupplierVM> GetAllSuppliers();
        bool CreateSupplier(Supplier obj);
        Supplier GetSupplierById(int? id);
        bool UpdateSupplier(Supplier obj);
        bool DeleteSupplierById(int supId);
    }
}

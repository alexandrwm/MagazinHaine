using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.BusinessLogic.Interfaces
{
    public interface IMgmtSup
    {
        List<SupplierVM> GetAllSuppliers();
        bool CreateSupplier(Supplier obj);
        Supplier GetSupplierById(int? id);
        bool UpdateSupplier(Supplier obj);
        bool DeleteSuplierById(int supId);
    }
}

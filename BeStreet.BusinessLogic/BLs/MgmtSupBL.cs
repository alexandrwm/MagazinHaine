using BeStreet.BusinessLogic.Core;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.ViewModels;
using System.Collections.Generic;

namespace BeStreet.BusinessLogic.BLs
{
    public class MgmtSupBL : MgmtSupApi, IMgmtSup
    {
        public bool CreateSupplier(Supplier obj)
        {
            return CreateSupplierAction(obj);
        }

        public bool DeleteSupplierById(int supId)
        {
            return DeleteSupplierByIdAction(supId);
        }

        public List<SupplierVM> GetAllSuppliers()
        {
            return GetAllSuppliersAction();
        }

        public Supplier GetSupplierById(int? id)
        {
            return GetSupplierByIdAction(id);
        }

        public bool UpdateSupplier(Supplier obj)
        {
            return UpdateSupplierAction(obj);
        }
    }
}

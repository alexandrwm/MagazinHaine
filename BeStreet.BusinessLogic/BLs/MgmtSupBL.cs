using BeStreet.BusinessLogic.Core;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.BusinessLogic.BLs
{
    public class MgmtSupBL : MgmtSupApi, IMgmtSup
    {
        public bool CreateSupplier(Supplier obj)
        {
            return CreateSupplierAction(obj);
        }

        public bool DeleteSuplierById(int supId)
        {
            return DeleteSuplierByIdAction(supId);
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

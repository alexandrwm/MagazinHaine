using BeStreet.BusinessLogic.DbContexts;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BeStreet.BusinessLogic.Core
{
    public class MgmtSupApi
    {
        public List<SupplierVM> GetAllSuppliersAction()
        {
            using (var db = new BeStreetContext())
            {
                var SupplierVM = from sup in db.Suppliers

                                 select new SupplierVM
                                 {
                                     SupId = sup.SupId,
                                     SupName = sup.SupName,
                                     SupTel = sup.SupTel,
                                     SupEmail = sup.SupEmail,
                                     SupAdd = sup.SupAdd,
                                     SupRemark = sup.SupRemark,
                                 };
                return SupplierVM.ToList();
            }
        }

        public bool CreateSupplierAction(Supplier obj)
        {
            using (var db = new BeStreetContext())
            {
                var sup = db.Suppliers.FirstOrDefault(s => s.SupName == obj.SupName);
                if (sup != null) return false;

                db.Suppliers.Add(obj);
                db.SaveChanges();
            }
            return true;
        }

        public Supplier GetSupplierByIdAction(int? id)
        {
            if (id == null) return null;
            Supplier sup;
            using (var db = new BeStreetContext())
            {
                sup = db.Suppliers.FirstOrDefault(s => s.SupId == id);
            }
            return sup;
        }

        internal bool UpdateSupplierAction(Supplier obj)
        {
            using (var db = new BeStreetContext())
            {
                var sup = db.Suppliers.FirstOrDefault(s => s.SupId == obj.SupId);
                if (sup == null) return false;

                sup.SupName = obj.SupName;
                sup.SupTel = obj.SupTel;
                sup.SupEmail = obj.SupEmail;
                sup.SupAdd = obj.SupAdd;
                sup.SupRemark = obj.SupRemark;

                db.SaveChanges();
            }
            return true;
        }

        internal bool DeleteSupplierByIdAction(int id)
        {
            using (var db = new BeStreetContext())
            {
                var sup = db.Suppliers.FirstOrDefault(s => s.SupId == id);
                if (sup == null) return false;

                db.Suppliers.Remove(sup);
                db.SaveChanges();
            }
            return true;
        }
    }
}

using BeStreet.BusinessLogic.DbContexts;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BeStreet.BusinessLogic.Core
{
    public class ProdApi
    {
        internal int CreateProductAction(Product obj)
        {
            int theid = -1;
            using (var db = new BeStreetContext())
            {
                db.Products.Add(obj);
                db.SaveChanges();
                theid = obj.PdId;
            }
            return theid;
        }

        internal List<PdFilterVM> GetProductsAction(string category)
        {
            using (var db = new BeStreetContext())
            {
                var pdvm = from p in db.Products

                           join pt in db.ProductTypes on p.PdtId equals pt.PdtId into join_p_pt

                           from p_pt in join_p_pt.DefaultIfEmpty()

                           join color in db.Colors on p.ColorId equals color.ColorId into join_p_color
                           from p_color in join_p_color.DefaultIfEmpty()

                           join size in db.Sizes on p.SizeId equals size.SizeId into join_p_size
                           from p_size in join_p_size.DefaultIfEmpty()

                           join target in db.Targets on p.TargetId equals target.TargetId into join_p_target
                           from p_target in join_p_target.DefaultIfEmpty()

                           join status in db.Statuses on p.StatusId equals status.StatusId into join_p_status
                           from p_status in join_p_status.DefaultIfEmpty()

                           where p_target.TargetName.Equals(category)

                           select new PdFilterVM
                           {
                               PdId = p.PdId,
                               ColorId = p.ColorId,
                               ColorName = p_color.ColorName,
                               SizeId = p_size.SizeId,
                               SizeName = p_size.SizeName,
                               TargetName = p_target.TargetName,
                               PdName = p.PdName,
                               PdtName = p_pt.PdtName,
                               PdPrice = p.PdPrice,
                               PdStk = p.PdStk,
                               StatusName = p_status.StatusName,
                           };

                return pdvm.ToList();
            }
        }

        internal List<PdVM> GetAllProductsAction()
        {
            using (var db = new BeStreetContext())
            {
                var pdvm = from p in db.Products
                           join pt in db.ProductTypes on p.PdtId equals pt.PdtId into join_p_pt
                           from p_pt in join_p_pt.DefaultIfEmpty()

                           join color in db.Colors on p.ColorId equals color.ColorId into join_p_color
                           from p_color in join_p_color.DefaultIfEmpty()

                           join size in db.Sizes on p.SizeId equals size.SizeId into join_p_size
                           from p_size in join_p_size.DefaultIfEmpty()


                           join target in db.Targets on p.TargetId equals target.TargetId into join_p_target
                           from p_target in join_p_target.DefaultIfEmpty()

                           join status in db.Statuses on p.StatusId equals status.StatusId into join_p_status
                           from p_status in join_p_status.DefaultIfEmpty()

                           join Sup in db.Suppliers on p.SupId equals Sup.SupId into join_p_Sup
                           from p_Sup in join_p_Sup.DefaultIfEmpty()

                           select new PdVM
                           {
                               PdId = p.PdId,
                               ColorId = p.ColorId,
                               SizeId = p.SizeId,
                               ColorName = p_color.ColorName,
                               SizeName = p_size.SizeName,
                               TargetName = p_target.TargetName,
                               PdName = p.PdName,
                               PdtName = p_pt.PdtName,
                               PdPrice = p.PdPrice,
                               PdStk = p.PdStk,
                               StatusName = p_status.StatusName,
                               SupName = p_Sup.SupName
                           };

                return pdvm.ToList();
            }
        }

        internal Product GetProductByIdAction(int? id)
        {
            if (id == null) return null;
            Product prod;
            using (var db = new BeStreetContext())
            {
                prod = db.Products.Find(id);
            }
            return prod;
        }

        internal bool DeleteProductByIdAction(int id)
        {
            using (var db = new BeStreetContext())
            {
                var prod = db.Products.Find(id);
                if (prod == null) return false;

                db.Products.Remove(prod);
                db.SaveChanges();
            }
            return true;
        }

        internal bool UpdateProductByIdAction(int? id, Product obj)
        {
            if (id == null) return false;

            using (var db = new BeStreetContext())
            {
                var prod = db.Products.Find(id);
                if (prod == null) return false;

                prod.PdName = obj.PdName;
                prod.PdPrice = obj.PdPrice;
                prod.PdStk = obj.PdStk;
                prod.PdDtls = obj.PdDtls;

                db.Entry(prod).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
            }
            return true;
        }
    }
}

using Abp.Linq.Expressions;
using BeStreet.BusinessLogic.DbContexts;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.ViewModels;
using JetBrains.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
                var pdvm = from p in db.Products where p.PdStk > 0

                           join pt in db.ProductTypes on p.PdtId equals pt.PdtId into join_p_pt

                           from p_pt in join_p_pt.DefaultIfEmpty()

                           join color in db.Colors on p.ColorId equals color.ColorId into join_p_color
                           from p_color in join_p_color.DefaultIfEmpty()

                           join size in db.Sizes on p.SizeId equals size.SizeId into join_p_size
                           from p_size in join_p_size.DefaultIfEmpty()

                           join target in db.Targets on p.TargetId equals target.TargetId into join_p_target
                           from p_target in join_p_target.DefaultIfEmpty()

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
    
        internal List<TEntity> GetFilterAction<TEntity>() where TEntity : class
        {
            using (var db = new BeStreetContext())
            {
                return db.Set<TEntity>().ToList();
            }
        }
    
        internal List<PdFilterVM> GetFilteredProductsAction(int[] typeIds, int[] sizeIds, int[] colorIds, string targetName)
        {
            var pr = PredicateBuilder.New<Product>();

            if (!(typeIds == null))
            {
                pr = pr.And(p => typeIds.Contains(p.PdtId));
            }
            if (!(sizeIds == null))
            {
                pr = pr.And(p => sizeIds.Contains(p.SizeId));
            }
            if (!(colorIds == null))
            {
                pr = pr.And(p => colorIds.Contains(p.ColorId));
            }
            pr.And(p => true);

            using (var db = new BeStreetContext())
            {
                var productFilter = from p in db.Products.Where(pr)
                                    join pt in db.ProductTypes on p.PdtId equals pt.PdtId into join_p_pt
                                    from p_pt in join_p_pt.DefaultIfEmpty()

                                    join color in db.Colors on p.ColorId equals color.ColorId into join_p_color
                                    from p_color in join_p_color.DefaultIfEmpty()

                                    join size in db.Sizes on p.SizeId equals size.SizeId into join_p_size
                                    from p_size in join_p_size.DefaultIfEmpty()

                                    join target in db.Targets on p.TargetId equals target.TargetId into join_p_target
                                    from p_target in join_p_target.DefaultIfEmpty()

                                    where p_target.TargetName == targetName

                                    select new PdFilterVM
                                    {
                                        PdId = p.PdId,
                                        ColorId = p.ColorId,
                                        ColorName = p_color.ColorName,
                                        SizeId = p.SizeId,
                                        SizeName = p_size.SizeName,
                                        TargetId = p.TargetId,
                                        TargetName = p_target.TargetName,
                                        PdName = p.PdName,
                                        PdtId = p.PdtId,
                                        PdtName = p_pt.PdtName,
                                        PdPrice = p.PdPrice,
                                        PdStk = p.PdStk,
                                    };
                return productFilter.ToList();
            }
        }

        internal List<PdFilterVM> GetDetailedProductByIdAction(int id)
        {
            using (var db = new BeStreetContext())
            {
                var productFilter = from p in db.Products
                                    join pt in db.ProductTypes on p.PdtId equals pt.PdtId into join_p_pt
                                    from p_pt in join_p_pt.DefaultIfEmpty()

                                    join color in db.Colors on p.ColorId equals color.ColorId into join_p_color
                                    from p_color in join_p_color.DefaultIfEmpty()

                                    join size in db.Sizes on p.SizeId equals size.SizeId into join_p_size
                                    from p_size in join_p_size.DefaultIfEmpty()

                                    join target in db.Targets on p.TargetId equals target.TargetId into join_p_target
                                    from p_target in join_p_target.DefaultIfEmpty()

                                    where p.PdId == id

                                    select new PdFilterVM
                                    {
                                        PdId = p.PdId,
                                        ColorId = p.ColorId,
                                        ColorName = p_color.ColorName,
                                        SizeId = p.SizeId,
                                        SizeName = p_size.SizeName,
                                        TargetId = p.TargetId,
                                        TargetName = p_target.TargetName,
                                        PdName = p.PdName,
                                        PdtId = p.PdtId,
                                        PdtName = p_pt.PdtName,
                                        PdPrice = p.PdPrice,
                                        PdStk = p.PdStk,
                                    };

                return productFilter.ToList();
            }
        }
    }
}

using BeStreet.BusinessLogic.BLs;
using BeStreet.BusinessLogic.DbContexts;
using BeStreet.Domain.Entities.Carts;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.BusinessLogic.Core
{
    public class CartApi
    {
        internal GetCartResp GetCurrentCartAction(int CusId)
        {
            using (var db = new BeStreetContext())
            {
                var existingCart = db.Carts.FirstOrDefault(c => c.CusId == CusId && c.CartCf != "Y");

                if (existingCart != null) return new GetCartResp { AlreadyExists = true, Cart = existingCart };
                Cart obj = new Cart()
                {
                    CusId = CusId,
                    CartDate = DateTime.Now,
                    CartQty = 0,
                    CartMoney = 0,
                    CartCf = "N",
                    CartPay = "N",
                    CartSend = "N",
                    CartVoid = "N"
                };
                db.Carts.Add(obj);
                db.SaveChanges();

                return new GetCartResp { AlreadyExists = false, Cart = obj };
            }
        }

        internal AddItemResp AddItemToCartAction(int cartid, int pdid, int qty)
        {
            using(var db = new BeStreetContext())
            {
                var cdtl = db.CartDtls.FirstOrDefault(cd => cd.CartId == cartid && cd.PdId == pdid);
                var pd = db.Products.FirstOrDefault(p => p.PdId == pdid);

                if (pd == null) return new AddItemResp { Status = false };

                if (cdtl != null)
                {
                    cdtl.CdtlQty += qty;
                    cdtl.CdtlMoney = pd.PdPrice * (cdtl.CdtlQty);
                } 
                else
                {
                    CartDtl obj = new CartDtl
                    {
                        CartId = cartid,
                        PdId = pdid,
                        CdtlQty = qty,
                        CdtlPrice = pd.PdPrice,
                        CdtlMoney = pd.PdPrice * qty
                    };
                    db.CartDtls.Add(obj);
                }
                db.SaveChanges();

                var cart = db.Carts.FirstOrDefault(c => c.CartId == cartid);
                if (cart == null) return new AddItemResp { Status = false };

                cart.CartMoney += pd.PdPrice * qty;
                cart.CartQty += qty;
                db.SaveChanges();

                return new AddItemResp
                {
                    Status = true,
                    CartMoney = (double)cart.CartMoney,
                    CartQty = (int)cart.CartQty
                };
            }
        }

        internal List<CtdVM> GetAllItemsAction(int cartid)
        {
            using (var db = new BeStreetContext())
            {
                var cartdtl = from ctd in db.CartDtls

                              join p in db.Products on ctd.PdId equals p.PdId
                                  into join_ctd_p
                              from ctd_p in join_ctd_p.DefaultIfEmpty()


                              join color in db.Colors on ctd_p.ColorId equals color.ColorId
                                  into join_ctd_color
                              from ctd_color in join_ctd_color.DefaultIfEmpty()

                              join size in db.Sizes on ctd_p.SizeId equals size.SizeId
                                  into join_ctd_size
                              from ctd_size in join_ctd_size.DefaultIfEmpty()

                              where ctd.CartId == cartid
                              select new CtdVM
                              {
                                  CartId = ctd.CartId,
                                  PdId = ctd.PdId,
                                  PdName = ctd_p.PdName,
                                  ColorName = ctd_color.ColorName,
                                  SizeName = ctd_size.SizeName,
                                  CdtlMoney = ctd.CdtlMoney,
                                  CdtlPrice = ctd.CdtlPrice,
                                  CdtlQty = ctd.CdtlQty
                              };
                return cartdtl.ToList();
            }

        }

        internal bool DeleteCartAction(int CusId)
        {
            using (var db = new BeStreetContext())
            {
                var cart = db.Carts.FirstOrDefault(c => c.CusId == CusId && c.CartCf == "N");
                if (cart == null) return false;

                var detail = from ctd in db.CartDtls
                             where ctd.CartId == cart.CartId
                             select ctd;
                foreach (var item in detail)
                {
                    db.CartDtls.Remove(item);
                }
                db.Carts.Remove(cart);
                db.SaveChanges();
            }
            return true;
        }

        internal AddItemResp DeleteItemFromCartAction(int cartid, int pdid)
        {
            using (var db = new BeStreetContext())
            {
                var cdtl = db.CartDtls.FirstOrDefault(cd => cd.CartId == cartid && cd.PdId == pdid);
                if (cdtl == null) return new AddItemResp { Status = false };
                
                var pd = db.Products.FirstOrDefault(p => p.PdId == pdid);
                if (pd == null) return new AddItemResp { Status = false };

                var cart = db.Carts.FirstOrDefault(c => c.CartId == cartid);
                if (pd == null) return new AddItemResp { Status = false };

                cart.CartQty -= cdtl.CdtlQty;
                cart.CartMoney -= cdtl.CdtlMoney;

                db.CartDtls.Remove(cdtl);
                if (cart.CartQty <= 0)
                {
                    db.Carts.Remove(cart);
                }
                db.SaveChanges();

                return new AddItemResp
                {
                    Status = true,
                    CartMoney = (double)cart.CartMoney,
                    CartQty = (int)cart.CartQty
                };
            }
        }

        internal bool ConfirmCartAction(int CusId)
        {
            using (var db = new BeStreetContext())
            {
                var cart = db.Carts.FirstOrDefault(c => c.CusId == CusId && c.CartCf == "N");
                if (cart == null) return false;

                var cartdtl = from ctd in db.CartDtls
                              where ctd.CartId == cart.CartId
                              select ctd;
                if (!cartdtl.Any()) return false;

                foreach (var detail in cartdtl)
                {
                    Product pd = db.Products.Find(detail.PdId);
                    pd.PdStk -= detail.CdtlQty;
                    pd.PdLastSale = DateTime.Now;
                }

                cart.CartCf = "Y";
                db.SaveChanges();
            }
            return true;
        }

        internal List<Cart> GetCartHistoryAction(int CusId)
        {
            using (var db = new BeStreetContext())
            {
                var cart = from c in db.Carts
                           where c.CusId == CusId && c.CartCf == "Y"
                           orderby c.CartId descending
                           select c;

                return cart.ToList();
            }
        }

        internal bool PayCartAction(int cartid)
        {
            using (var db = new BeStreetContext())
            {
                var cartdtl = from ctd in db.CartDtls
                              where ctd.CartId == cartid
                              select ctd;

                if (!cartdtl.Any()) return false;

                var master = db.Carts.Find(cartid);
                master.CartPay = "Y";
                master.CartSend = "Y";
                db.SaveChanges();
            }
            return true;
        }

        internal Cart GetCartByIdAction(int cartid, int CusId)
        {
            using (var db = new BeStreetContext())
            {
                var cart = db.Carts.FirstOrDefault(c => c.CartId == cartid && c.CusId == CusId);
                return cart;
            }
        }
    }
}

using BeStreet.BusinessLogic.Core;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.Carts;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.BusinessLogic.BLs
{
    public class CartBL : CartApi, ICart
    {
        public AddItemResp AddItemToCart(int cartid, int pdid, int qty)
        {
            return AddItemToCartAction(cartid, pdid, qty);
        }

        public List<CtdVM> GetAllItems(int cartid)
        {
            return GetAllItemsAction(cartid);
        }

        public GetCartResp GetCurrentCart(int CusId)
        {
            return GetCurrentCartAction(CusId);
        }

        public bool DeleteCart(int CusId)
        {
            return DeleteCartAction(CusId);
        }

        public AddItemResp DeleteItemFromCart(int cartid, int pdid)
        {
            return DeleteItemFromCartAction(cartid, pdid);
        }

        public bool ConfirmCart(int CusId)
        {
            return ConfirmCartAction(CusId);
        }

        public List<Cart> GetCartHistory(int CusId)
        {
            return GetCartHistoryAction(CusId);
        }

        public bool PayCart(int cartid)
        {
            return PayCartAction(cartid);
        }

        public Cart GetCartById(int cartid, int CusId)
        {
            return GetCartByIdAction(cartid, CusId);
        }
    }
}

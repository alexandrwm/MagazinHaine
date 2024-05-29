using BeStreet.Domain.Entities.Carts;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeStreet.BusinessLogic.Interfaces
{
    public interface ICart
    {
        GetCartResp GetCurrentCart(int CusId);
        AddItemResp AddItemToCart(int cartid, int pdid, int qty);
        List<CtdVM> GetAllItems(int cartid);
        bool DeleteCart(int CusId);
        AddItemResp DeleteItemFromCart(int cartid, int pdid);
        bool ConfirmCart(int CusId);
        List<Cart> GetCartHistory(int CusId);
        bool PayCart(int cartid);
        Cart GetCartById(int cartid, int CusId);
    }
}

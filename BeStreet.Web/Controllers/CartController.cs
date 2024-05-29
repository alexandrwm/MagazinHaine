using BeStreet.BusinessLogic.DbContexts;
using BeStreet.Domain.Entities.Items;
using System;
using System.Linq;
using System.Data.Entity;
using System.Dynamic;
using System.Globalization;
using System.Web.Mvc;
using BeStreet.Domain.Entities.ViewModels;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.BusinessLogic;
using BeStreet.Web.Controllers;
namespace BeStreet.Controllers
{
    public class CartController : BaseController
    {
        private readonly ICart _cart = new BusinesLogic().GetCartBL();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddDtl(int? pdid, int qty)
        {
            var currentUrl = (string)Session["CurrentUrl"];

            if (Session["UserId"] == null)
            {
                TempData["ErrorMessage"] = "Conectați-vă înainte de a cumpăra produse";
                return RedirectToAction("Login", "Home");
            }

            if (pdid == null)
            {
                TempData["ErrorMessage"] = "Trebuie să specificați codul produsului.";
                return RedirectToAction("Index", "Home");
            }

            if (Session["CartId"] == null)
            {
                return RedirectToAction("Add", new { pdid, qty });
            }

            var resp = _cart.AddItemToCart((int)Session["CartId"], (int)pdid, qty);
            
            Session["CartQty"] = resp.CartQty.ToString();
            Session["CartMoney"] = resp.CartMoney.ToString();

            return Redirect(currentUrl);
        }

        public ActionResult Add(int pdid, int qty)
        {
            int CusId = (int)Session["UserId"];
            
            try
            {
                Session["CartId"] = _cart.GetCurrentCart(CusId).Cart.CartId;
                Session["CartQty"] = "0";
                Session["CartMoney"] = "0";

                return RedirectToAction("AddDtl", new { pdid, qty });
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Eroare de înregistrare.";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Show(int? cartid)
        {
            if (Session["UserId"] == null)
            {
                TempData["ErrorMessage"] = "Va rugam sa va logati";
                return RedirectToAction("Login", "Home");
            }

            int cusid = (int)Session["UserId"];


            var cart = (cartid == null) ? 
                _cart.GetCurrentCart(cusid).Cart : 
                _cart.GetCartById((int)cartid, cusid);
            
            if (cart == null)
            {
                TempData["ErrorMessage"] = "Coșul specificat nu a fost găsit.";
                return RedirectToAction("Index", "Home");
            }

            var cartdtl = _cart.GetAllItems(cart.CartId);

            dynamic DyModel = new ExpandoObject();

            DyModel.Master = cart;
            DyModel.Detail = cartdtl;

            return View(DyModel);
        }

        public ActionResult Delete()
        {
            if (Session["UserId"] == null)
            {
                TempData["ErrorMessage"] = "Va rugam sa va logati";
                return RedirectToAction("Login", "Home");
            }

            var deleted = _cart.DeleteCart((int)Session["UserId"]);
            if (!deleted)
            {
                TempData["ErrorMessage"] = "Coșul nu a fost găsit.";
                return RedirectToAction("Show", "Cart");
            }
            Session.Remove("CartId");
            Session.Remove("CartQty");
            Session.Remove("CartMoney");

            TempData["SuccessMessage"] = "Comanda anulata.";
            return RedirectToAction("Index", "Home");
        }

        public ActionResult DeleteDtl(int pdid)
        {
            if (Session["UserId"] == null)
            {
                TempData["ErrorMessage"] = "Va rugam sa va logati";
                return RedirectToAction("Login", "Home");
            }

            var cart = _cart.GetCurrentCart((int)Session["UserId"]);
            var resp = _cart.DeleteItemFromCart(cart.Cart.CartId, pdid);

            if (!resp.Status)
            {
                TempData["ErrorMessage"] = "Nu au fost găsite informații.";
            }
            if (resp.CartQty <= 0)
            {
                Session.Remove("CartId");
                Session.Remove("CartQty");
                Session.Remove("CartMoney");

                TempData["SuccessMessage"] = "Comanda anulata.";
                return RedirectToAction("Index", "Home");
            }
            else
            {
                Session["CartMoney"] = resp.CartMoney.ToString();
                Session["CartQty"] = resp.CartQty.ToString();
                return RedirectToAction("Show", "Cart");
            }
        }

        public ActionResult Confirm()
        {
            if (Session["UserId"] == null)
            {
                TempData["ErrorMessage"] = "Va rugam sa va logati";
                return RedirectToAction("Login", "Home");
            }

            var cusid = (int)Session["UserId"];
            var confirm = _cart.ConfirmCart(cusid);

            if (!confirm)
            {
                TempData["ErrorMessage"] = "Eroare de confirmare.";
                return RedirectToAction("Show", "Cart");
            }

            HttpContext.Session.Remove("CartId");
            HttpContext.Session.Remove("CartQty");
            HttpContext.Session.Remove("CartMoney");

            TempData["SuccessMessage"] = "Comandă confirmată.";
            return RedirectToAction("List", "Cart");
            //return RedirectToAction("Index", "Home");
        }

        public ActionResult List()
        {
            var user = _session.GetUserByCookie(Request.Cookies["UserCookie"].Value);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Va rugam sa va logati.";
                return RedirectToAction("Login", "Home");
            }

            var cart = _cart.GetCartHistory(user.Id);
            return View(cart);
        }

        public ActionResult Paid(int cartid)
        {
            var user = _session.GetUserByCookie(Request.Cookies["UserCookie"].Value);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Va rugam sa va logati.";
                return RedirectToAction("Login", "Home");
            }

            var cart = _cart.GetCartById(cartid, user.Id);

            var hasItems = _cart.PayCart(cart.CartId);
            if (!hasItems)
            {
                TempData["ErrorMessage"] = "Plata eșuată.";
                return RedirectToAction("Show", "Cart");
            }


            TempData["SuccessMessage"] = "Plată efectuată";
            return RedirectToAction("Show", "Cart", new { cartid = cart.CartId });
        }
    }
}

using System.Globalization;
using BeStreet.Models;
using System.Dynamic;
using System.Web.Mvc;
using System;
using System.Collections.Generic;
namespace BeStreet.Controllers
{
    public class CartController : Controller
    {
       
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AddDtl(string pdid,int qty)
        {
			Console.WriteLine("AddDetail Function");
			Console.WriteLine($"Product Id: {pdid}");
			Console.WriteLine($"Quantity: {qty}");
			var currentUrl = Session["CurrentUrl"];

			if (Session["CusId"] == null)
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
                return RedirectToAction("Add", new { pdid = pdid ,qty = qty});
            }

            string CartId = (string)Session["CartId"];

            

			return Redirect((string)currentUrl);

		}
        public ActionResult Add(string pdid, int qty)
        {
            string theId;
            int rowCount = 0;
            int i = 0;
            string today;
            string CusId = (string)Session["CusId"];

            CultureInfo us = new CultureInfo("en-US");
            do
            {
                i++;
                today = DateTime.Now.ToString("'CT'yyyyMMdd");
                theId = string.Concat(today, i.ToString("0000"));

              
             
            } while (rowCount != 0);

            try
            {
                Cart obj = new Cart();
                obj.CartId = theId;
                obj.CusId = CusId;
                obj.CartDate = (DateTime.Now.Date);

                obj.CartQty = 0;
                obj.CartMoney = 0;


                Session["CartId"] = theId;
                Session["CartQty"] = "0";
                Session["CartMoney"] = "0";

                return RedirectToAction("AddDtl", new { pdid = pdid , qty = qty});
            }catch (Exception ex) {
                TempData["ErrorMessage"] = "Eroare de înregistrare.";
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult Show(string cartid)
        {
            if (cartid == null)
            {
                TempData["ErrorMessage"] = "Trebuie specificată valoarea Id cos.";
                return RedirectToAction("Index");
            }
            if (Session["CusId"] == null)
            {
                TempData["ErrorMessage"] = "Va rugam sa va logati";
                return RedirectToAction("Index");
            }
            
            string cusid = (string)Session["CusId"];
           
            //if (cart == null)
            //{
            //    TempData["ErrorMessage"] = "Coșul specificat nu a fost găsit.";
            //    return RedirectToAction("Index", "Home");
            //}
           
            //if (CartCheckID!.CusId != cusid)
            //{
            //    TempData["ErrorMessage"] = "Nu aveți permisiunea de a accesa datele.";
            //    return RedirectToAction("List", new { cusid = cusid });
            //}

           

            dynamic DyModel = new ExpandoObject();

            //DyModel.Master = cart;
            //DyModel.Detail = cartdtl;

            return View(DyModel);
        }

        public ActionResult Check()
        {
            string cusid = (string)Session["CusId"];

            //int rowCount = cart.Count();
            int rowCount = 0;
            Console.WriteLine($"===========================");
            Console.WriteLine($"CountRow: {rowCount}");
            if (rowCount > 0)
            {
                Cart obj = new Cart();
                //foreach(var item in cart)
                //{
                //    obj = item;
                //}
                Session["CartId"] = obj.CartId;
                Session["CartQty"] = obj.CartQty.ToString();
                Session["CartMoney"] = obj.CartMoney.ToString();
            }
            return RedirectToAction("Index","Home");
        }

        public ActionResult Delete(string cartid) {
           
            //if(master == null)
            {
                TempData["ErrorMessage"] = "Coșul nu a fost găsit.";
                //return RedirectToAction("Show","Cart",new {cartid = cartid});
            }
           

            Session.Remove("CartId");
            Session.Remove("CartQty");
            Session.Remove("CartMoney");

            TempData["SuccessMessage"] = "Comanda anulata.";
            return RedirectToAction("Index", "Home");
        }
        public ActionResult DeleteDtl(string pdid,string cartid)
        {
           
            //if(obj == null)
            {
                TempData["ErrorMessage"] = "Nu au fost găsite informații.";
            }
        
          
            //if(cartqty == 0)
            {

                HttpContext.Session.Remove("CartId");
                HttpContext.Session.Remove("CartQty");
                HttpContext.Session.Remove("CartMoney");

                TempData["SuccessMessage"] = "Comanda anulata.";
                return RedirectToAction("Index", "Home");

            }
            //else
            {

                //HttpContext.Session.SetString("CartMoney", cartmoney.ToString());
                //HttpContext.Session.SetString("CartQty", cartqty.ToString());
                //return RedirectToAction("Show","Cart",new {cartid = cartid});
            }
        }

        public ActionResult Confirm(string cartid)
        {
            var cartdtl = new List<object>();
            int rowCount = cartdtl.Count;
            if(rowCount == 0)
            {
                TempData["ErrorMessage"] = "Eroare de confirmare.";
                return RedirectToAction("show", "Cart", new { cartid = cartid });
            }


            HttpContext.Session.Remove("CartId");
            HttpContext.Session.Remove("CartQty");
            HttpContext.Session.Remove("CartMoney");

            TempData["SuccessMessage"] = "Comandă confirmată.";
            //return RedirectToAction("List", "Cart", new { cusid = master.CusId });
            return RedirectToAction("Index", "Home");
        }
        public ActionResult List(string cusid)
        {
            if (cusid == null)
            {
                TempData["ErrorMessage"] = "Valoarea necesară pentru id.";
                return RedirectToAction("Index");
            }
            if (Session["CusId"] == null)
            {
                TempData["ErrorMessage"] = "Va rugam sa va logati.";
                return RedirectToAction("Index");
            }
            if (Session["CusId"] as string != cusid)
            {
                TempData["ErrorMessage"] = "Nu aveți permisiunea de a accesa datele.";
                return RedirectToAction("List", new { cusid = Session["CusId"] });
            }
            var cart = new List<Cart>();
            return View(cart);
        }
        public ActionResult Paid(string cartid)
        {
            var cartdtl = new List<CartDtl>();
            int rowCount = cartdtl.Count;
            if (rowCount == 0)
            {
                TempData["ErrorMessage"] = "Plata eșuată.";
                return RedirectToAction("show", "Cart", new { cartid = cartid });
            }

            //update cart for Paid
            var master = new Cart();



            TempData["SuccessMessage"] = "Plată efectuată";
            return RedirectToAction("List", "Cart", new { cusid = master.CusId });
        }
    }
}

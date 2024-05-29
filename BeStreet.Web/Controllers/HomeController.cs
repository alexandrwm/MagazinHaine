using BeStreet.BusinessLogic;
using BeStreet.BusinessLogic.DbContexts;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.User;
using BeStreet.Models;
using BeStreet.Web.Controllers;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace BeStreet.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IProd _prods = new BusinesLogic().GetProdBL();
        private readonly ICart _cart = new BusinesLogic().GetCartBL();
        public ActionResult Index()
        {
            SessionStatus();
            ViewData["NoContainerClass"] = true;
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
            //return RedirectToAction("Shop");
        }

        public ActionResult Privacy()
        {
            SessionStatus();
            return View();
        }

        public ActionResult Login()
        {
            SessionStatus();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserLogin login)
        {
            if (ModelState.IsValid)
            {
                ULoginData data = new ULoginData()
                {
                    Login = login.ULogin,
                    Pass = login.UPass,
                    LastLogin = DateTime.Now
                };
                var userLogin = _session.UserLogin(data);
                if (userLogin.Status)
                {

                    HttpCookie cookie = _session.GetCookie(login.ULogin);

                    ControllerContext.HttpContext.Response.Cookies.Add(cookie);

                    Session["UserName"] = userLogin.Name;
                    Session["UserId"] = userLogin.Id;
                    
                    var resp = _cart.GetCurrentCart(userLogin.Id);
                    if (resp.AlreadyExists)
                    {
                        var cart = resp.Cart;
                        Session["CartId"] = cart.CartId;
                        Session["CartMoney"] = cart.CartMoney;
                        Session["CartQty"] = cart.CartQty;
                    }

                    if (userLogin.Role == Domain.Enums.URole.Customer)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (userLogin.Role == Domain.Enums.URole.Admin)
                    {
                        return RedirectToAction("Index", "Product");
                    }
                }
                else
                {
                    TempData["ErrorMessage"] = "Numele de utilizator sau parola este incorecta.";
                    return RedirectToAction("Login");
                }
            }

            //return RedirectToAction("Index");
            return RedirectToAction("Check", "Cart");

        }
        public ActionResult Logout()
        {
            EatCookie();
            HttpContext.Session.Clear();
            HttpContext.Session.Abandon();
            return RedirectToAction("Index");
        }

        public ActionResult Shop()
        {
            SessionStatus();
            return View();
        }
        public ActionResult Men()
        {
            SessionStatus();

            var pdvm = _prods.GetProducts("Barbati");
            if (pdvm == null) return HttpNotFound();
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            ViewData["FilterCategory"] = _prods.GetFilter<ProductType>();
            ViewData["FilterColor"] = _prods.GetFilter<Color>();
            ViewData["FilterSize"] = _prods.GetFilter<Size>();
            return View(pdvm);
        }

        public ActionResult Women()
        {
            SessionStatus();

            var pdvm = _prods.GetProducts("Femei");
            if (pdvm == null) return HttpNotFound();
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            ViewData["FilterCategory"] = _prods.GetFilter<ProductType>();
            ViewData["FilterColor"] = _prods.GetFilter<Color>();
            ViewData["FilterSize"] = _prods.GetFilter<Size>();
            
            return View(pdvm);
        }

        public ActionResult Kids()
        {
            SessionStatus();

            var pdvm = _prods.GetProducts("Copii");
            if (pdvm == null) return HttpNotFound();
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

            ViewData["FilterCategory"] = _prods.GetFilter<ProductType>();
            ViewData["FilterColor"] = _prods.GetFilter<Color>();
            ViewData["FilterSize"] = _prods.GetFilter<Size>();

            return View(pdvm);
        }

        public ActionResult Register()
        {
            SessionStatus();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserRegister obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    URegData data = new URegData
                    {
                        Name = obj.UName,
                        Login = obj.ULogin,
                        Pass = obj.UPass,
                        Email = obj.UEmail,
                        StartDate = DateTime.Now,
                        LastLogin = DateTime.Now
                    };

                    var userReg = _session.UserReg(data);

                    if (!userReg)
                    {
                        ViewBag.ErrorMessage = "Numele de utilizator deja există.";
                        return View(obj);
                    }

                    TempData["SuccessMessage"] = "Inregistrat cu succes!";

                    return RedirectToAction("Login", "Home");
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(obj);
            }

            ViewBag.ErrorMessage = "Eroare la înregistrare.";
            return View(obj);
        }
    }
}

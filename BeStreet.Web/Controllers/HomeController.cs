using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using BeStreet.BusinessLogic;
using BeStreet.BusinessLogic.DbContexts;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.User;
using BeStreet.Models;
using BeStreet.ViewModels;


namespace BeStreet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession _session;
        private readonly IProd _prods;
        public HomeController()
        {
            _session = new BusinesLogic().GetSessionBL();
            _prods = new BusinesLogic().GetProdBL();
        }
        public ActionResult Index()
        {
            ViewData["NoContainerClass"] = true;
            //var recommend = _session.GetRecommendation();

            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
            //return RedirectToAction("Shop");
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult Login()
        {
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
                    Session["UserName"] = userLogin.Name;
                    Session["UserId"] = userLogin.Id;
                    if (userLogin.Role == Domain.Enums.URole.Customer)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else if (userLogin.Role == Domain.Enums.URole.Admin)
                    {
                        return RedirectToAction("Index", "Admin");
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
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Shop(string stext)
        {
                return RedirectToAction("Index");
        }

        public ActionResult Shop()
        {
         
            return View();
        }
        public ActionResult Men()
        {
            using (var db = new BeStreetContext())
            {
                var FilterCategory = db.ProductTypes.ToList();
                var FilterColor = db.Colors.ToList();
                var FilterSize = db.Sizes.ToList();

                var pdvm = _prods.GetProducts("Barbati");
                if (pdvm == null) return HttpNotFound();
                ViewBag.ErrorMessage = TempData["ErrorMessage"];
                
                ViewData["FilterCategory"] = FilterCategory;
                ViewData["FilterColor"] = FilterColor;
                ViewData["FilterSize"] = FilterSize;
                return View(pdvm);
            }
        }

		//[HttpPost]
		//[ValidateAntiForgeryToken]
  //      public ActionResult Men(string stext)
		//{
		//	if (stext == null)
		//	{
		//		return RedirectToAction("men");
		//	}
			
		//	return View();
		//}

        public ActionResult Women()
        {
			
			return View();
        }

        public ActionResult Kids()
        {
			
			return View();
        }

        public ActionResult Register()
        {
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
    
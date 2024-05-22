using System;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using BeStreet.BusinessLogic;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.User;
using BeStreet.Models;
using BeStreet.ViewModels;


namespace BeStreet.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISession _session;
        public HomeController()
        {
            _session = new BusinesLogic().GetSessionBL();
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
                    CusLogin = login.CusLogin,
                    CusPass = login.CusPass,
                    LastLogin = DateTime.Now
                };
                var userLogin = _session.UserLogin(data);
                if (userLogin)
                {
                    Session["CusName"] = data.CusLogin;
                    Session["CusId"] = "1";
                    return RedirectToAction("Index", "Home");
                }
                else return HttpNotFound();
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

			return View();
        }

		[HttpPost]
		[ValidateAntiForgeryToken]
        public ActionResult Men(string stext)
		{
			if (stext == null)
			{
				return RedirectToAction("men");
			}
			
			return View();
		}

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
                        CusName = obj.CusName,
                        CusLogin = obj.CusLogin,
                        CusPass = obj.CusPass,
                        CusEmail = obj.CusEmail,
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
    
using System;
using System.Runtime.Remoting.Messaging;
using System.Web.Mvc;
using MagazinHaine.BusinessLogic;
using MagazinHaine.BusinessLogic.Interfaces;
using MagazinHaine.Domain.Entities.User;
using MagazinHaine.Models;


namespace MagazinHaine.Controllers
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
            

            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View();
            //return RedirectToAction("Shop");
        }

        public ActionResult Privacy()
        {
            return View();
        }

        public ActionResult Error()
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
                    Username = login.Username,
                    Password = login.Password,
                    Ip = Request.UserHostAddress,
                    LoginTime = DateTime.Now
                };
                var userLogin = _session.UserLogin(data);
                if (userLogin)
                {
                    Session["CusName"] = data.Username;
                    Session["CusId"] = 1;
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
        public ActionResult men()
        {

			return View();
        }
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult men(string stext)
		{
			if (stext == null)
			{
				return RedirectToAction("men");
			}
			
			return View();
		}

		public ActionResult women()
        {
			
			return View();
        }

        public ActionResult kids()
        {
			
			return View();
        }

        public ActionResult Register()
        {
            return View();
        }

    }
}
    
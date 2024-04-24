using System;
using System.Web.Mvc;


namespace MagazinHaine.Controllers
{
    public class HomeController : Controller
    {
      

        public HomeController()
        {
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
        public ActionResult Login(string userName, string userPass)
        {

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
    
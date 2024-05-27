using System;
using System.Web.Mvc;

namespace BeStreet.Web.Controllers
{
    public class OrderController : Controller
    {


        public ActionResult OrderToDay()
        {
            DateTime todayDate = DateTime.Now;
            return RedirectToAction("Index", new { date = todayDate });
        }

        public ActionResult Index(DateTime? date)
        {
            return View();
        }


        [HttpPost] //si aici trebuie sa sterg sau ce e asta
        [ValidateAntiForgeryToken]
        public ActionResult Index(DateTime filterDate)
        {
            return RedirectToAction("Index", new { date = filterDate });
        }

        public ActionResult Show(string CartId)
        {


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdatePay(string CartId, string CartPay)
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateVoid(string CartId, string CartVoid)
        {

            return View();
        }
    }
}

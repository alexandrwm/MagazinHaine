using BeStreet.ViewModels;
using System;
using System.Web.Mvc;

namespace BeStreet.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult SalesDailyProducts()
        {
            return RedirectToAction("Login", "Home");
        }

        /*   ViewData["currentNav"] = "Report/Product";

           DateTime thedate = DateTime.(DateTime.Now.Date);

           ViewBag.theDate = thedate.ToString("yyyy-MM-dd");
           return View(rep);*/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SalesDailyProducts(DateTime thedate)
        {
            

          //  ViewData["currentNav"] = "Report/Product";
           

          //  ViewBag.theDate = thedate.ToString("yyyy-MM-dd");
          return View();
        }
        public ActionResult SaleMonthlyProducts()
        {
           

           // ViewData["currentNav"] = "Report/MonthlyProduct";

            var theMonth = DateTime.Now.Month;
            var theYear = DateTime.Now.Year;

            DateTime sDate = new DateTime(theYear, theMonth, 1);

            DateTime eDate = new DateTime(theYear, theMonth, 1).AddMonths(1).AddDays(-1);


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaleMonthlyProducts(DateTime sDate, DateTime eDate)
        {
           
          
            return View();
        }
        public ActionResult SaleDailyCustomer()
        {
           

           // ViewData["currentNav"] = "Report/Purchase";
            DateTime thedate = DateTime.Now;


           
            ViewBag.theDate = thedate.ToString("yyyy-MM-dd");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaleDailyCustomer(DateTime  thedate)
        {


            ViewData["currentNav"] = "Report/Purchase";

            ViewBag.theDate = thedate.ToString("yyyy-MM-dd");
            return View();
        }
        public ActionResult SaleMonthlyCustomer()
        {
        

            ViewData["currentNav"] = "Report/MonthlyPurchase";
            
            var theMonth = DateTime.Now.Month;
            var theYear = DateTime.Now.Year;

            DateTime sDate = new DateTime(theYear, theMonth, 1);

            DateTime eDate = new DateTime   (theYear, theMonth, 1).AddMonths(1).AddDays(-1);

          
            ViewBag.sDate = sDate.ToString("yyyy-MM-dd");
            ViewBag.eDate = eDate.ToString("yyyy-MM-dd");

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SaleMonthlyCustomer(DateTime sDate, DateTime    eDate)
        {
            

            ViewData["currentNav"] = "Report/MonthlyPurchase";
            ViewBag.sDate = sDate.ToString("yyyy-MM-dd");
            ViewBag.eDate = eDate.ToString("yyyy-MM-dd");

            return View();
        }
    }
}
// gata amush

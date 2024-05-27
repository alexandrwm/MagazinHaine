using BeStreet.Domain.Entities.Items;
using System;
using System.Dynamic;
using System.Globalization;
using System.Web.Mvc;

namespace KuShop.Controllers
{
    public class BuyingController : Controller
    {


        public ActionResult Index()
        {
            if (Session["StfId"] == null)
            {
                TempData["ErrorMessage"] = "Id numar incorect.";
                return RedirectToAction("Login", "Home");
            }

            ViewData["currentNav"] = "Product/Buying";

            var theMonth = DateTime.Now.Month;
            var theYear = DateTime.Now.Year;
            DateTime sDate = new DateTime(theYear, theMonth, 1);

            DateTime eDate = new DateTime(theYear, theMonth, 1).AddMonths(1).AddDays(-1);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(DateTime sDate, DateTime eDate)
        {
            if (Session["StfId"] == null)
            {

                TempData["ErrorMessage"] = "Id numar incorect.";
                return RedirectToAction("Login", "Home");
            }


            return View();
        }

        public ActionResult Create()
        {
            if (Session["StfId"] == null)
            {

                TempData["ErrorMessage"] = "Id numar incorect.";
                return RedirectToAction("Login", "Home");
            }

            ViewData["currentNav"] = "Product/Buying";

            string theId;
            int rowCount = 0;
            int i = 0;
            CultureInfo us = new CultureInfo("en-US");
            do
            {
                i++;
                theId = string.Concat(DateTime.Now.ToString("'BUY'yyyyMMdd", us), i.ToString("00"));

            }
            while (rowCount != 0);

            ViewBag.BuyId = theId;


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Buying obj)
        {
            if (Session["StfId"] == null)
            {
                TempData["ErrorMessage"] = "Id numar incorect.";
                return RedirectToAction("Login", "Home");
            }

            ViewData["currentNav"] = "Product/Buying";

            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Show", "Buying");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Eroare de înregistrare";
                return View(obj);
            }
            TempData["ErrorMessage"] = "Eroare de înregistrare";
            return View(obj);
        }

        public ActionResult Show(string buyid)
        {

            if (HttpContext.Session["StfId"] == null)
            {

                TempData["ErrorMessage"] = "Id numar incorect.";
                return RedirectToAction("Login", "Home");
            }

            ViewData["currentNav"] = "Product/Buying";

            if (buyid == null)
            {
                TempData["ErrorMessage"] = "Trebuie să specificați numărul";
                return RedirectToAction("Index");
            }


            ViewBag.theid = buyid;

            dynamic DyModel = new ExpandoObject();

            return View(DyModel);
        }

        public ActionResult Edit(string buyid)
        {
            if (Session["StfId"] == null)
            {
                TempData["ErrorMessage"] = "Id numar incorect.";
                return RedirectToAction("Login", "Home");
            }

            ViewData["currentNav"] = "Product/Buying";
            if (buyid == null)
            {
                TempData["ErrorMessage"] = "Trebuie să specificați numărul";
                return RedirectToAction("Index");
            }


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Buying obj)
        {

            if (Session["StfId"] == null)
            {

                TempData["ErrorMessage"] = "Id numar incorect.";
                return RedirectToAction("Login", "Home");
            }

            ViewData["currentNav"] = "Product/Buying";
            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Show", "Buying");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Corectarea erorii";
                return View(obj);
            }
            TempData["ErrorMessage"] = "Corectarea erorii";
            return View(obj);
        }

        public ActionResult Delete(string buyid)
        {
            if (Session["StfId"] == null)
            {
                TempData["ErrorMessage"] = "Id numar incorect.";
                return RedirectToAction("Login", "Home");
            }


            return RedirectToAction("Index");
        }

        public ActionResult CreateDtl(string buyid)
        {
            if (Session["StfId"] == null)
            {

                TempData["ErrorMessage"] = "Id numar incorect.";
                return RedirectToAction("Login", "Home");
            }

            if (buyid == null)
            {
                TempData["ErrorMessage"] = "Trebuie să specificați numărul.";
                return RedirectToAction("Show", new { buyid = buyid });
            }



            ViewBag.BuyId = buyid;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateDtl(BuyDtl obj)
        {
            if (Session["StfId"] == null)
            {
                TempData["ErrorMessage"] = "Id numar incorect.";
                return RedirectToAction("Login", "Home");
            }

            try
            {
                if (ModelState.IsValid)
                {
                    return RedirectToAction("Show", "Buying", new { buyid = obj.BuyId });
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Eroare de înregistrare";
                return RedirectToAction("Show", "Buying", new { buyid = obj.BuyId });
            }
            TempData["ErrorMessage"] = "Eroare de înregistrare";
            return RedirectToAction("Show", "Buying", new { buyid = obj.BuyId });
        }

        public ActionResult DeleteDtl(string pdid, string buyid)
        {
            if (HttpContext.Session["StfId"] == null)
            {
                TempData["ErrorMessage"] = "Kun mai me sit na naka";
                return RedirectToAction("Login", "Home");
            }





            return RedirectToAction("Show", "Buying", new { buyid = buyid });
        }
    }

    public class Buying
    {
    }
}

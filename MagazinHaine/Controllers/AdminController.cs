using System.Diagnostics;
using System;
using System.Web.Mvc;
using System.IO;
using ClothesShop.Models;

namespace ClothesShop.Controllers
{
    public class AdminController : Controller
    {
        
        public ActionResult Index()
        {
            if (Session["StfId"] == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult login(string userName, string userPass)
        {
           

            string StfId;
            string StfName;
            string DutyId;
            

            return RedirectToAction("Index", "Order");

        }

        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Show(string id)
        {
            if (Session["StfId"] == null)
            {

                TempData["ErrorMessage"] = "Id numar incorect.";
                return RedirectToAction("Login", "Home");
            }

            var currentStfID = Session["StfId"];
            if (currentStfID != id)
            {
                TempData["ErrorMessage"] = "Id numar incorect.";
                return RedirectToAction("Show", "Admin", new { id = currentStfID });
            }


            if (id == null)
            {
                TempData["ErrorMessage"] = "Specificati Id Nr.";
                return RedirectToAction("Index");
            }

          

            var fileName = id.ToString() + ".png";
            var imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgEmployee");
            var filePath = Path.Combine(imgPath, fileName);
            if (System.IO.File.Exists(filePath))
            {
                ViewBag.ImgFile = "/imgEmployee/" + id + ".png";
            }
            else
            {
                ViewBag.ImgFile = "/image/No_image2.png";
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Staff obj)
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
                   
                    TempData["SuccessMessage"] = "Salvat cu succes!";
                    Session["StfId"] = obj.StfId;
                    Session["StfName"]= obj.StfName;
                    Session["DutyId"] = obj.DutyId;
                    return RedirectToAction("Show", "Admin", new { id = obj.StfId });

                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(obj);
            }
            ViewBag.ErrorMessage = "Corectarea erorii";
            //ViewData["Pdt"] = new SelectList(_db.ProductTypes, "PdtId", "PdtName", obj.PdId);
            //ViewData["Brand"] = new SelectList(_db.Brands, "BrandId", "BrandName", obj.BrandId);
            return View(obj);
        }
        public ActionResult ImgUpload( string theid)
        {
            var FileName = theid;
          
            var FileExtension = ".png";
            var SaveFileName = FileName + FileExtension;
            var SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgEmployee");
            var SaveFilePath = Path.Combine(SavePath, SaveFileName);

            using (FileStream fs = System.IO.File.Create(SaveFilePath))
            {

            }
            TempData["SuccessMessage"] = "Imaginea a fost incarcata cu succes!";
            return RedirectToAction("Show", "Admin", new { id = theid });
        }

        public ActionResult ImgDelete(string id)
        {
            var fileName = id.ToString() + ".png";
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgEmployee");
            var filePath = Path.Combine(imagePath, fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            TempData["SuccessMessage"] = "Imaginea a fost stearsa cu succes!";
            return RedirectToAction("Show", "Admin" , new { id = id });
        }
    }
}

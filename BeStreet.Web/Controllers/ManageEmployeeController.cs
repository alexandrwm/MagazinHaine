using BeStreet.Domain.Entities.Items;
using BeStreet.Models;
using BeStreet.ViewModels;
using System;
using System.IO;
using System.Web.Mvc;

namespace BeStreet.Controllers
{
    public class ManageEmployeeController : Controller
    {

        public ActionResult Index()
        {
           return View();
        }

        public ActionResult Create()
        {

            ViewData["currentNav"] = "ManageEmployee/Create";
                
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( string obj)
        {
    
                return View(obj);
           
        }
        public ActionResult Create(Staff obj)
        {

            TempData["SuccessMessage"] = "Creat cu succes!";
            // Redirect to appropriate action after successful registration
            //return RedirectToAction("Edit", "ManageCustomers", new { id = nextCustomerId });

            return RedirectToAction("Edit", "ManageEmployee", new { id = obj.StfId });
            //return View();
        }


        public ActionResult Edit(string id)
        {
           

            ViewData["currentNav"] = "ManageEmployee";

            if (id == null)
            {
                TempData["ErrorMessage"] = "Specificați Id Nr.";
                return RedirectToAction("Index");
            }

            {
                TempData["ErrorMessage"] = "Nu au fost găsite informații.";
                return RedirectToAction("Index");
            }

           
        }
      

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Staff obj)
        {

            return View();
        }

        public ActionResult ImgUpload( string theid)
        {
            

            var FileName = theid;
            //var FileExtension = Path.GetExtension(imgfiles.FileName);
            var FileExtension = ".png";
            var SaveFileName = FileName + FileExtension;
            var SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgEmployee");
            var SaveFilePath = Path.Combine(SavePath, SaveFileName);

            using (FileStream fs = System.IO.File.Create(SaveFilePath))
            {
                //imgfiles.CopyTo(fs);
                fs.Flush();
            }
            TempData["SuccessMessage"] = "Imagine incarcata cu succes!";
            return RedirectToAction("Edit", new { id = theid });
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
            TempData["SuccessMessage"] = "Imagine stearsa cu succes!";
            return RedirectToAction("Edit", new { id = id });
        }

        public ActionResult Delete(string id)
        {
           

            ViewData["currentNav"] = "ManageCustomers";

            if (id == null)
            {
                TempData["ErrorMessage"] = "Specificați Id Nr.";
                return RedirectToAction("Index");
            }

            string obj = null;
            if (obj == null)
            {
                TempData["ErrorMessage"] = "Nu au fost găsite informații.";
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
                //ViewBag.ImgFile = "/imgcus/No_image2.png";
                ViewBag.ImgFile = "/image/No_image2.png";
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(string StfId)
        {
            return View(); 

        
        }
    }
}

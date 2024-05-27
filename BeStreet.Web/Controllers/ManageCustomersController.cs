using BeStreet.ViewModels;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace BeStreet.Controllers
{
    public class ManageCustomersController : Controller
    {

        public ActionResult Index()
        {

            ViewData["currentNav"] = "ManageCustomers";

            return View();
        }
        public ActionResult Create()
        {

            ViewData["currentNav"] = "ManageCustomers/Create";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string blabla)
        {

            return View();
        }

        public ActionResult Edit(string id)
        {


            ViewData["currentNav"] = "ManageCustomers";

            if (id == null)
            {
                TempData["ErrorMessage"] = "Specificați Id Nr.";
                return RedirectToAction("Index");
            }



            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            return View();
        }



        public ActionResult ImgUpload(string theid)
        {


            var FileName = theid;
            //var FileExtension = Path.GetExtension(imgfiles.FileName);
            var FileExtension = ".png";
            var SaveFileName = FileName + FileExtension;
            var SavePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgcus");
            var SaveFilePath = Path.Combine(SavePath, SaveFileName);

            using (FileStream fs = System.IO.File.Create(SaveFilePath))
            {
                // imgfiles.CopyTo(fs); 
                fs.Flush();
            }
            TempData["SuccessMessage"] = "Imagine incarcata cu succes!";
            return RedirectToAction("Edit", new { id = theid });
        }

        public ActionResult ImgDelete(string id)
        {


            var fileName = id.ToString() + ".png";
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgcus");
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
                TempData["ErrorMessage"] = "Specificați ID-ul.";
                return RedirectToAction("Index");
            }
            string obj = null;

            if (obj == null)
            {
                TempData["ErrorMessage"] = "Nu au fost găsite informații.";
                return RedirectToAction("Index");
            }

            var fileName = id.ToString() + ".png";
            var imgPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\imgcus");
            var filePath = Path.Combine(imgPath, fileName);
            if (System.IO.File.Exists(filePath))
            {
                ViewBag.ImgFile = "/imgcus/" + id + ".png";
            }
            else
            {
                //ViewBag.ImgFile = "/imgcus/No_image2.png";
                ViewBag.ImgFile = "/image/No_image2.png";
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(string CusId)
        {
            return View();
        }
    }
}
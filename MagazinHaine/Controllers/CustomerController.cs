using System;
using System.IO;
using System.Web.Mvc;
using MagazinHaine.Models;

namespace MagazinHaine.Controllers
{
    public class CustomerController : Controller
    {
        public CustomerController() {
        }
        public ActionResult Index()
        {
            return View();
        }
		public ActionResult Show(string id)
		{
			if (id == null)
			{
				TempData["ErrorMessage"] = "Valoarea necesară id.";
				return RedirectToAction("Index");
			}
            if (Session["CusId"] == null)
            {
                TempData["ErrorMessage"] = "Va rugam sa va logati.";
                return RedirectToAction("Index");
            }
            if (Session["CusId"] as string != id)
            {
                TempData["ErrorMessage"] = "Nu aveți permisiunea de a accesa datele.";
                return RedirectToAction("Show", new { id = Session["CusId"] });
            }
			var obj = new Customer();
			if (obj == null)
			{
				TempData["ErrorMessage"] = "Id Nr. nu este găsit.";
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
				ViewBag.ImgFile = "/image/No_image2.png";
			}
			return View(obj);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Customer obj)
		{
			try
			{
				if (ModelState.IsValid)
				{
					Session["CusName"] = obj.CusName;
                    //return RedirectToAction("Index");
                    TempData["SuccessMessage"] = "Salvat cu succes!";
					return RedirectToAction("Show", "Customer", new { id = obj.CusId });
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
			//return View(obj);
			return RedirectToAction("Show", "Customer", new { id = obj.CusId });
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
			return RedirectToAction("Show", new { id = id });
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
				//imgfiles.CopyTo(fs);
				fs.Flush();
			}
			TempData["SuccessMessage"] = "Imagine incarcata cu succes!";
            return RedirectToAction("Show", new { id = theid });
		}
	}
}
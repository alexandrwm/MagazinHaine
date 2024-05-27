using BeStreet.BusinessLogic;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.User;
using BeStreet.Web.Filters;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace BeStreet.Controllers
{
    [AllowAdmin]
    public class ManageCustomersController : Controller
    {
        private readonly IMgmtCus _mgmt = new BusinesLogic().GetMgmtCusBL();
        public ActionResult Index()
        {

            ViewData["currentNav"] = "ManageCustomers";

            var customerVM = _mgmt.GetAllCustomers();

            if (customerVM == null) return HttpNotFound();
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View(customerVM);
        }
        public ActionResult Create()
        {
            ViewData["currentNav"] = "ManageCustomers/Create";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Customer obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    obj.LastLogin = DateTime.Now;
                    obj.StartDate = DateTime.Now;
                    var userCreated = _mgmt.CreateCustomer(obj);

                    if (!userCreated)
                    {
                        ViewBag.ErrorMessage = "Id utilizatorului deja exista.";
                        return View(obj);
                    }

                    TempData["SuccessMessage"] = "Creat cu succes.";

                    //return RedirectToAction("Edit", "ManageCustomers", new { id = nextCustomerId });
                    return RedirectToAction("Index");
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

        public ActionResult Edit(int? id)
        {
            ViewData["currentNav"] = "ManageCustomers";

            if (id == null)
            {
                TempData["ErrorMessage"] = "Specificați Id Nr.";
                return RedirectToAction("Index");
            }

            var obj = _mgmt.GetCustomerById(id);
            
            if (obj == null)
            {
                TempData["ErrorMessage"] = "Nu au fost găsite informații.";
                return RedirectToAction("Index");
            }

            var fileName = "C" + id.ToString() + ".png";
            var imgPath = Path.Combine(Server.MapPath("~/wwwroot/"), "imgcus");
            var filePath = Path.Combine(imgPath, fileName);
            if (System.IO.File.Exists(filePath))
            {
                ViewBag.ImgFile = "/wwwroot/imgcus/" + fileName;
            }
            else
            {
                ViewBag.ImgFile = "/wwwroot/imgpd/No_image2.png";
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
                    _mgmt.UpdateCustomer(obj);
                    //return RedirectToAction("Index");
                    TempData["SuccessMessage"] = "Salvat cu succes!";
                    return RedirectToAction("Edit", "ManageCustomers", new { id = obj.Id });
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(obj);
            }
            ViewBag.ErrorMessage = "Corectarea erorii.";
            //ViewData["Pdt"] = new SelectList(_db.ProductTypes, "PdtId", "PdtName", obj.PdId);
            //ViewData["Brand"] = new SelectList(_db.Brands, "BrandId", "BrandName", obj.BrandId);
            //return View(obj);
            return RedirectToAction("Edit", "ManageCustomers", new { id = obj.Id });
        }

        public ActionResult Delete(int? id)
        {
            ViewData["currentNav"] = "ManageCustomers";

            if (id == null)
            {
                TempData["ErrorMessage"] = "Specificați ID-ul.";
                return RedirectToAction("Index");
            }
            var obj = _mgmt.GetCustomerById(id);

            if (obj == null)
            {
                TempData["ErrorMessage"] = "Nu au fost găsite informații.";
                return RedirectToAction("Index");
            }

            var fileName = "C" + id.ToString() + ".png";
            var imgPath = Path.Combine(Server.MapPath("~/wwwroot/"), "imgcus");
            var filePath = Path.Combine(imgPath, fileName);
            if (System.IO.File.Exists(filePath))
            {
                ViewBag.ImgFile = "/wwwroot/imgcus/" + fileName;
            }
            else
            {
                ViewBag.ImgFile = "/wwwroot/imgpd/No_image2.png";
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int Id)
        {
            try
            {
                var cusDeleted = _mgmt.DeleteCustomerById(Id);
                if (!cusDeleted)
                {
                    TempData["ErrorMessage"] = "Nu s-au șters datele.";
                    return RedirectToAction("Index");
                }

                TempData["SuccessMessage"] = "Delete SuccessMessage";
                return RedirectToAction("Index");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "Nu s-au șters datele.";
                return RedirectToAction("Index");
            }
        }
        public ActionResult ImgUpload(HttpPostedFileBase imgfiles, int theid)
        {

            if (imgfiles?.ContentLength > 0)
            {
                var FileName = "C" + theid.ToString();
                var FileExtension = ".png";
                var SaveFileName = FileName + FileExtension;
                var SavePath = Path.Combine(Server.MapPath("~/wwwroot/"), "imgcus");
                var SaveFilePath = Path.Combine(SavePath, SaveFileName);

                imgfiles.SaveAs(SaveFilePath);
            }

            TempData["SuccessMessage"] = "Imagine incarcata cu succes!";
            return RedirectToAction("Edit", new { id = theid });
        }

        public ActionResult ImgDelete(int id)
        {

            var fileName = "C" + id.ToString() + ".png";
            var imagePath = Path.Combine(Server.MapPath("~/wwwroot/"), "imgcus");
            var filePath = Path.Combine(imagePath, fileName);
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            TempData["SuccessMessage"] = "Imagine stearsa cu succes!";
            return RedirectToAction("Edit", new { id = id });
        }
    }
}
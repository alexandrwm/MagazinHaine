

using BeStreet.BusinessLogic;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.Items;
using System;
using System.Web.Mvc;

namespace BeStreet.Web.Controllers
{
    public class ManageSuppliersController : Controller
    {
        private readonly IMgmtSup _mgmt = new BusinesLogic().GetMgmtSupBL();
        public ActionResult Index()
        {
            ViewData["currentNav"] = "ManageSuppliers";

            var SupplierVM = _mgmt.GetAllSuppliers();

            if (SupplierVM == null) return HttpNotFound();
            
            ViewBag.ErrorMessage = TempData["ErrorMessage"];
            return View(SupplierVM);
        }
        public ActionResult Create()
        {
            ViewData["currentNav"] = "ManageSuppliers/Create";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Supplier obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var supCreated = _mgmt.CreateSupplier(obj);
                    //var existingCustomer = _db.Suppliers.FirstOrDefault(c => c.SupId == obj.SupId);
                    if (!supCreated)
                    {
                        ViewBag.ErrorMessage = "Furnizorul deja exista";
                        return View(obj);
                    }

                    TempData["SuccessMessage"] = "Creat cu succes!";

                    //return RedirectToAction("Edit", "ManageSuppliers", new { id = nextCustomerId });
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
            ViewData["currentNav"] = "ManageSuppliers";

            if (id == null)
            {
                TempData["ErrorMessage"] = "Specificați Id Nr.";
                return RedirectToAction("Index");
            }

            var obj = _mgmt.GetSupplierById(id);
            if (obj == null)
            {
                TempData["ErrorMessage"] = "Nu au fost găsite informații.";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Supplier obj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var supUpdated = _mgmt.UpdateSupplier(obj);
                    //return RedirectToAction("Index");
                    return RedirectToAction("Edit", "ManageSuppliers", new { id = obj.SupId });
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(obj);
            }
            ViewBag.ErrorMessage = "Corectarea erorii.";
            return View(obj);
        }


        public ActionResult Delete(int? id)
        {
            ViewData["currentNav"] = "ManageSuppliers";

            if (id == null)
            {
                TempData["ErrorMessage"] = "Specificați Id Nr.";
                return RedirectToAction("Index");
            }
            var obj = _mgmt.GetSupplierById(id);

            if (obj == null)
            {
                TempData["ErrorMessage"] = "Nu au fost găsite informații.";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeletePost(int SupId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var supDeleted = _mgmt.DeleteSuplierById(SupId);
                    if (!supDeleted)
                    {
                        TempData["ErrorMessage"] = "Nu s-au șters datele.";
                        return RedirectToAction("Index");
                    }
                    
                    TempData["SuccessMessage"] = "Delete SuccessMessage";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Nu s-au șters datele: " + ex.Message;
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}



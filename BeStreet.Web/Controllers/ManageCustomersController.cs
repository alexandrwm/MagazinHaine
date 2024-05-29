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
    }
}
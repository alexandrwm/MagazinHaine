using System;
using System.IO;
using System.Web.Mvc;
using BeStreet.BusinessLogic;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Entities.Items;
using BeStreet.Domain.Entities.User;

namespace BeStreet.Controllers
{
    public class CustomerController : Controller
    {
		private readonly ISession _session = new BusinesLogic().GetSessionBL();
        public CustomerController() {
        }
        public ActionResult Index()
        {
            return View();
        }
		public ActionResult Show(int? id)
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
            if ((int)Session["CusId"] != id)
            {
                TempData["ErrorMessage"] = "Nu aveți permisiunea de a accesa datele.";
                return RedirectToAction("Show", new { id = Session["CusId"] });
			}
			var obj = _session.GetUserProf(id);

			if (obj == null)
			{
				TempData["ErrorMessage"] = "Id Nr. nu este găsit.";
				return RedirectToAction("Index");
			}

			return View(obj);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(UserProfile obj)
		{
            try
            {
                if (ModelState.IsValid)
                {
                    var cusId = _session.UserUpdateByUsername(obj, obj.CusLogin);
                    if (cusId == null) throw new Exception("Salvarea a esuat!");
                    Session["CusName"] = obj.CusName;
                    
                    TempData["SuccessMessage"] = "Salvat cu succes!";
                    return RedirectToAction("Show", "Customer", new { id = cusId });
                }
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
                return View(obj);
            }
            
            ViewBag.ErrorMessage = "Corectarea erorii";
            return RedirectToAction("Show", "Customer", new { id = (int)Session["CusId"] });
        }
    }
}
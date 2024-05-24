using System;
using System.Web.Mvc;
using System.IO;
using System.Data.Entity;
using BeStreet.Domain.Entities.Items;
using BeStreet.BusinessLogic.DbContexts;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.BusinessLogic;
using BeStreet.Domain.Entities.User;

namespace BeStreet.Controllers
{
    public class AdminController : Controller
    {
        //private readonly IAdmin _adminSession = new BusinesLogic().GetAdminBL();
        public ActionResult Index()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }
    }
}

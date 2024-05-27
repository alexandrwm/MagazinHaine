using System.Web.Mvc;

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

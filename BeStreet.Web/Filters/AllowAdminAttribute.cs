using BeStreet.BusinessLogic;
using BeStreet.BusinessLogic.Interfaces;
using BeStreet.Domain.Enums;
using System.Web;
using System.Web.Mvc;

namespace BeStreet.Web.Filters
{
    public class AllowAdminAttribute : ActionFilterAttribute
    {
        private readonly ISession _session = new BusinesLogic().GetSessionBL();

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var cookie = HttpContext.Current.Request.Cookies["UserCookie"];
            if (cookie != null)
            {
                var user = _session.GetUserByCookie(cookie.Value);
                if (user == null) filterContext.Result = new HttpNotFoundResult();
                else
                {
                    if (user.Role != URole.Admin) filterContext.Result = new HttpNotFoundResult();
                }
            }
            else filterContext.Result = new HttpNotFoundResult();
        }
    }
}
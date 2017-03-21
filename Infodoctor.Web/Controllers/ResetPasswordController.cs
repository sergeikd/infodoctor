using System.Collections.Generic;
using System.Web.Mvc;

namespace Infodoctor.Web.Controllers
{
    public class ResetPasswordController : Controller
    {
        // GET: ResetPassword
        public ActionResult Index(string email, string code)
        {
            var paramList = new List<string>()
            {
                email,code
            };

            return View(paramList);
        }
    }
}
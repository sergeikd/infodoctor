using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;

namespace Infodoctor.Web.Controllers
{
    public class UserValidationController : Controller
    {
        private ApplicationUserManager _userManager;

        public UserValidationController()
        {
            
        }

        public UserValidationController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? Request.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }
        }

        // GET: ResetPassword
        public ActionResult ResetPassword(string email, string code)
        {
            var paramList = new List<string>()
            {
                email,code
            };

            return View(paramList);
        }

        // GET: ConfirmEmail
        public async Task<ActionResult> ConfirmEmail(string email, string code)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user != null)
            {
                user.EmailConfirmed = true;

                var result = await UserManager.UpdateAsync(user);
                if (!result.Succeeded)
                    return View(result);
            }

            return View();
        }
    }
}
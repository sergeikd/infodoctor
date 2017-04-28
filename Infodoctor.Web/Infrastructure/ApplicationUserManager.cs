using Infodoctor.Domain.Entities;
using Microsoft.AspNet.Identity;

namespace Infodoctor.Web.Infrastructure
{
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }
    }
}
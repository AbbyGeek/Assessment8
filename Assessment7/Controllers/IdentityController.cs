using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Assessment7.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace Assessment7.Controllers
{
    public class IdentityController : Controller
    {

        public UserManager<IdentityUser> UserManager => HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();


        // GET: Identity
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Registration(Registration user)
        {
            if (ModelState.IsValid)
            {
                //not detecting validity
                var identityResult = await UserManager.CreateAsync(new IdentityUser(user.Username), (user.Password));
                if (identityResult.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", identityResult.Errors.FirstOrDefault());
                    return View();
                }
            }

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().Get<UserManager<IdentityUser>>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                IdentityUser user = UserManager.Find(login.Username, login.Password);

                //creates an identity(ident) with the user. This is the cookie that keeps the user logged in
                if (user != null)
                {
                    var ident = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    //using our instance of authentication, we use the signin method
                    //IsPersistent = false will logout the user and terminate that cookie (ident)
                    authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);

                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", "Invalid Username Or Password");
            return View(login);



        }
    }
    
}
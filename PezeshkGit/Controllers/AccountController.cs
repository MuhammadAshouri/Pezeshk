using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using PezeshkGit.Data;
using PezeshkGit.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace PezeshkGit.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AccountController));
        readonly ApplicationDbContext _context;
        public AccountController() => _context = new ApplicationDbContext();
        protected override void Dispose(bool disposing) => _context.Dispose();

        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        // GET: Account/Login
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (AuthenticationManager.User.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Dashboard");
            return View();
        }

        // Post: Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel login)
        {
            var err = "نام کاربری یا رمز عبور نادرست میباشد.";
            var user = await _context.Admins.SingleOrDefaultAsync(u => u.Username == login.Username);

            if (user == null || user.Password != Hash.GetPasswordHash(login.Password))
            {
                ViewBag.LoginError = err;
                return View();
            }

            IdentitySignin(new AppUserState
            {
                UserId = user.ID,
                Username = user.Username,
                Email = user.Email,
                AdminType = user.AdminType,
                AddDate = user.AddDate,
                LastLogin = ""
            });

            Log.Info($"Admin with Username: {Identity.GetUsername()} LoggedIn.");
            return RedirectToAction("Index", "Dashboard");
        }

        // GET: Account/Register
        public ActionResult Register()
        {
            return View();
        }

        // GET: Account/Logout
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            IdentitySignout();
            Log.Info($"Admin with Username: {Identity.GetUsername()} LoggedOut.");
            return RedirectToAction("Index", "Home");
        }

        public void IdentitySignin(AppUserState appUserState, string providerKey = null, bool isPersistent = false)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, appUserState.UserId.ToString()),
                new Claim(ClaimTypes.Name, appUserState.Username),

                new Claim("userState", appUserState.ToString())
            };

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            AuthenticationManager.SignIn(new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = isPersistent,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);

            var user = _context.Admins.SingleOrDefault(c => c.ID == appUserState.UserId);
            user.LastLogin = Date.GetDateTime();
            _context.SaveChanges();
        }

        public void IdentitySignout() =>
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie,
                DefaultAuthenticationTypes.ExternalCookie);

        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            AppUserState appUserState = new AppUserState();

            ViewData["UserState"] = appUserState;
        }
    }
}
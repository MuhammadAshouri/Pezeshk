using PezeshkGit.Models.Enum;
using System.Web.Mvc;

namespace PezeshkGit.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            return View(Identity.GetAdminType().ToString());
        }

        public ActionResult Galleries(int? id)
        {
            if (Identity.IsInRole(AdminType.Secretary)) return RedirectToAction("Index", "Dashboard");
            else return View();
        }

        public ActionResult Posts(int? id)
        {
            if (Identity.IsInRole(AdminType.Secretary)) return RedirectToAction("Index", "Dashboard");
            else return View();
        }

        public ActionResult Categories()
        {
            if (Identity.IsInRole(AdminType.Secretary)) return RedirectToAction("Index", "Dashboard");
            else return View();
        }

        public ActionResult Messages(int? id)
        {
            if (Identity.IsInRole(AdminType.BlogManager)) return RedirectToAction("Index", "Dashboard");
            else return View();
        }

        public ActionResult Comments()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            if (Identity.IsInRole(AdminType.BlogManager)) return RedirectToAction("Index", "Dashboard");
            return View();
        }

        public ActionResult Admins()
        {
            if (Identity.IsInRole(AdminType.FullAdmin)) return View();
            return RedirectToAction("Index", "Dashboard");
        }
    }
}
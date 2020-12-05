using System.Web.Mvc;

namespace PezeshkGit.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            ViewBag.Title = "PageNotFound";
            return Content("PageNotFound");
        }

        public ActionResult InternalError()
        {
            ViewBag.Title = "InternalError";
            return Content("InternalError");
        }
    }
}
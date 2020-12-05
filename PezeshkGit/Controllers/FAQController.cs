using System.Web.Mvc;

namespace Pezeshk.Controllers
{
    public class FAQController : Controller
    {
        // GET: FAQ
        public ActionResult Index()
        {
            Api.FAQController con = new Api.FAQController();
            var aa = con.GetFAQs();
            
            return View();
        }
    }
}
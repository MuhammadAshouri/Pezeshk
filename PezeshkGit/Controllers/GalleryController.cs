using Pezeshk.Data;
using Pezeshk.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace Pezeshk.Controllers
{
    public class GalleryController : Controller
    {
        #region _context
        readonly ApplicationDbContext _context;
        public GalleryController() => _context = new ApplicationDbContext();
        protected override void Dispose(bool disposing) => _context.Dispose();
        #endregion

        // GET: Gallery
        public ActionResult Index()
        {
            var galleries = _context.Galleries.ToList();
            var categories = _context.Categories.ToList();
            var viewModel = new GalleryListViewModel
            {
                Galleries = galleries,
                Categories = categories
            };
            return View(viewModel);
        }
    }
}

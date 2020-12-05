using Pezeshk.Data;
using Pezeshk.Models.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Pezeshk.Controllers
{
    public class PostsController : Controller
    {
        #region _context
        readonly ApplicationDbContext _context;
        public PostsController() => _context = new ApplicationDbContext();
        protected override void Dispose(bool disposing) => _context.Dispose();
        #endregion

        // GET: Posts
        public ActionResult Index()
        {
            var posts = _context.Posts.ToList().FindAll(m => m.Published == true);
            var categories = _context.Categories.ToList();
            var viewModel = new PostListViewModel
            {
                Posts = posts,
                Categories = categories
            };
            return View(viewModel);
        }

        public ActionResult View(int? id)
        {
            string aa = "a" + id;
            if (id == null) throw new HttpException(404, "");
            var model = _context.Posts.Include(c => c.Comments).SingleOrDefault(c => c.ID == id);
            return View(model);
        }
    }
}
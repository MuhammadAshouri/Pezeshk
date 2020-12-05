using AutoMapper;
using log4net;
using PezeshkGit.Data;
using PezeshkGit.Dtos;
using PezeshkGit.Models;
using System.Linq;
using System.Web.Http;

namespace PezeshkGit.Controllers.Api
{
    [Authorize]
    public class CategoryController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CategoryController));
        #region _context
        readonly ApplicationDbContext _context;
        public CategoryController() => _context = new ApplicationDbContext();
        protected override void Dispose(bool disposing) => _context.Dispose();
        #endregion

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetCategories() =>
            Ok(_context.Categories.ToList().Select(Mapper.Map<Category, CategoryDto>));

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetCategory(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.ID == id);

            if (category == null) return NotFound();
            return Ok(Mapper.Map<Category, CategoryDto>(category));
        }

        [HttpPost]
        public IHttpActionResult CreateCategory(CategoryDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            categoryDto.AddDate = Date.GetDateTime();

            var category = Mapper.Map<CategoryDto, Category>(categoryDto);

            _context.Categories.Add(category);
            _context.SaveChanges();

            categoryDto.ID = category.ID;

            Log.Info($"Category with id: {category.ID} added by admin with Username: {Identity.GetUsername()}");
            return Ok(categoryDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateCategory(int id, CategoryDto categoryDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var catInDb = _context.Categories.SingleOrDefault(c => c.ID == id);

            if (catInDb == null) return NotFound();
            categoryDto.AddDate = catInDb.AddDate;

            Mapper.Map(categoryDto, catInDb);

            _context.SaveChanges();
            Log.Info($"Category with id: {id} updated by admin with Username: {Identity.GetUsername()}");
            return Ok(categoryDto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            var catInDb = _context.Categories.SingleOrDefault(c => c.ID == id);

            if (catInDb == null) return NotFound();

            _context.Categories.Remove(catInDb);
            _context.SaveChanges();
            Log.Info($"Category with id: {id} deleted by admin with Username: {Identity.GetUsername()}");
            return Ok();
        }
    }
}

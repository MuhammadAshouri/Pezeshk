using AutoMapper;
using log4net;
using Pezeshk.Data;
using Pezeshk.Dtos;
using Pezeshk.Models;
using System.Linq;
using System.Web.Http;

namespace Pezeshk.Controllers.Api
{
    [Authorize]
    public class GalleryController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(GalleryController));
        #region _context
        readonly ApplicationDbContext _context;
        public GalleryController() => _context = new ApplicationDbContext();
        protected override void Dispose(bool disposing) => _context.Dispose();
        #endregion

        [HttpPost]
        public IHttpActionResult AddToGallery(GalleryDto galleryDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            galleryDto.AddDate = Date.GetDateTime();
            galleryDto.LastUpdate = galleryDto.AddDate;

            var gallery = Mapper.Map<GalleryDto, Gallery>(galleryDto);

            _context.Galleries.Add(gallery);
            _context.SaveChanges();

            Log.Info($"Gallery with id: {gallery.ID} added by admin with Username: {Identity.GetUsername()}");
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult UpdateGallery(int id, GalleryDto galleryDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var galleryInDb = _context.Galleries.SingleOrDefault(c => c.ID == id);

            if (galleryInDb == null) return NotFound();
            galleryDto.LastUpdate = Date.GetDateTime();
            galleryDto.AddDate = galleryInDb.AddDate;

            Mapper.Map(galleryDto, galleryInDb);

            _context.SaveChanges();
            Log.Info($"Gallery with id: {id} updated by admin with Username: {Identity.GetUsername()}");
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetGallery(int id)
        {
            var gallery = _context.Galleries.SingleOrDefault(c => c.ID == id);

            if (gallery == null) return NotFound();
            return Ok(Mapper.Map<Gallery, GalleryDto>(gallery));
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetTopGalleries(int count) =>
            Ok(_context.Galleries.OrderByDescending(c => c.AddDate).Take(count).ToList()
                .Select(Mapper.Map<Gallery, GalleryDto>));

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetGalleries() => Ok(_context.Galleries.ToList()
            .Select(Mapper.Map<Gallery, GalleryDto>));

        [HttpDelete]
        public IHttpActionResult DeleteGallery(int id)
        {
            var galleryInDb = _context.Galleries.SingleOrDefault(c => c.ID == id);

            if (galleryInDb == null) return NotFound();

            _context.Galleries.Remove(galleryInDb);
            _context.SaveChanges();
            Log.Info($"Gallery with id: {id} deleted by admin with Username: {Identity.GetUsername()}");
            return Ok();
        }
    }
}

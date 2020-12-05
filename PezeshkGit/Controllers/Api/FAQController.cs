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
    public class FAQController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(FAQController));
        #region _context
        readonly ApplicationDbContext _context;
        public FAQController() => _context = new ApplicationDbContext();
        protected override void Dispose(bool disposing) => _context.Dispose();
        #endregion

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetFAQs() =>
            Ok(_context.FAQs.ToList().Select(Mapper.Map<FAQ, FAQDto>));

        [HttpGet]
        public IHttpActionResult GetFAQ(int id)
        {
            var faq = _context.FAQs.SingleOrDefault(c => c.ID == id);

            if (faq == null) return NotFound();
            return Ok(Mapper.Map<FAQ, FAQDto>(faq));
        }

        [HttpPut]
        public IHttpActionResult UpdateFAQ(int id, FAQDto faqDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var faqInDb = _context.FAQs.SingleOrDefault(c => c.ID == id);

            if (faqInDb == null) return NotFound();

            Mapper.Map(faqDto, faqInDb);

            _context.SaveChanges();

            Log.Info($"FAQ with id: {id} updated by admin with Username: {Identity.GetUsername()}");
            return Ok(faqDto);
        }

        [HttpPost]
        public IHttpActionResult AddFAQ(FAQDto faqDto)
        {
            if (!ModelState.IsValid) return BadRequest();
            var faq = Mapper.Map<FAQDto, FAQ>(faqDto);

            _context.FAQs.Add(faq);
            _context.SaveChanges();

            faqDto.ID = faq.ID;

            Log.Info($"FAQ with id: {faq.ID} added by admin with Username: {Identity.GetUsername()}");
            return Ok(faqDto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteFAQ(int id)
        {
            var faqInDb = _context.FAQs.SingleOrDefault(c => c.ID == id);

            if (faqInDb == null) return NotFound();

            _context.FAQs.Remove(faqInDb);
            _context.SaveChanges();
            Log.Info($"FAQ with id: {id} deleted by admin with Username: {Identity.GetUsername()}");
            return Ok();
        }
    }
}

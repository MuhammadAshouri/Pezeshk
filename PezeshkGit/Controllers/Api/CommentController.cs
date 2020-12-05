using AutoMapper;
using log4net;
using Pezeshk.Data;
using Pezeshk.Dtos;
using Pezeshk.Models;
using Pezeshk.Models.ViewModels;
using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Http;

namespace Pezeshk.Controllers.Api
{
    [Authorize]
    public class CommentController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(CommentController));
        #region _context
        readonly ApplicationDbContext _context;
        public CommentController() => _context = new ApplicationDbContext();
        protected override void Dispose(bool disposing) => _context.Dispose();
        #endregion

        public IHttpActionResult GetComments() =>
            Ok(_context.Comments.Include(c => c.Post).ToList()
                .Select(Mapper.Map<Comment, CommentDto>));

        [AllowAnonymous]
        public IHttpActionResult GetCommentsForShow(int postId, string start)
        {
            int c = 5;
            int s = int.Parse(start) * c;
            var post = _context.Posts.Include(q => q.Comments)
                .Select(Mapper.Map<Post, PostDto>).SingleOrDefault(q => q.ID == postId);
            if (post == null) return NotFound();

            var comments = post.Comments.Where(q => q.ShowInSite == true).OrderByDescending(q => q.Time);

            if (s + c > comments.Count()) c = comments.Count() - s;
            return Ok(comments.ToList().GetRange(s, c));
        }

        [AllowAnonymous]
        public IHttpActionResult GetCommentsCountForShow(int postId)
        {
            var post = _context.Posts.Include(q => q.Comments)
                .Select(Mapper.Map<Post, PostDto>).SingleOrDefault(q => q.ID == postId);
            if (post == null) return NotFound();

            var comments = post.Comments.Where(q => q.ShowInSite == true).Count();

            return Ok(comments);
        }

        public IHttpActionResult GetUnreadComments() =>
            Ok(_context.Comments.Where(c => c.ReadState == false).Include(c => c.Post)
                .ToList().Select(Mapper.Map<Comment, CommentDto>));

        public IHttpActionResult GetUnreadCommentsCount() =>
            Ok(_context.Comments.Where(c => c.ReadState == false).ToList().Count);

        public IHttpActionResult GetComment(int id)
        {
            var comment = _context.Comments.Include(c => c.Post).SingleOrDefault(c => c.ID == id);

            if (comment == null) return NotFound();
            return Ok(Mapper.Map<Comment, CommentDto>(comment));
        }

        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult AddComment(CommentDto CommentDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var comment = Mapper.Map<CommentDto, Comment>(CommentDto);

            comment.ReplyText = "";
            comment.Time = Date.GetDateTime();
            comment.Post = _context.Posts.SingleOrDefault(c => c.ID == CommentDto.Post_ID);

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Ok(CommentDto);
        }

        [HttpPut]
        public IHttpActionResult UpdateReadState(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var comment = _context.Comments.SingleOrDefault(c => c.ID == id);
            if (comment == null) return NotFound();
            comment.ReadState = true;

            return Ok(_context.SaveChanges());
        }

        [HttpPut]
        public IHttpActionResult UpdateShowInSite(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var comment = _context.Comments.SingleOrDefault(c => c.ID == id);
            if (comment == null) return NotFound();
            comment.ShowInSite = !comment.ShowInSite;

            Log.Info($"Comment with id: {id} showed in site by admin with Username: {Identity.GetUsername()}");
            return Ok(_context.SaveChanges());
        }

        [HttpPost]
        public IHttpActionResult ReplyMessage(int id, ReplyMessageViewModel viewModel)
        {
            try
            {
                var email = "testjust83@gmail.com";
                var pass = "JmFm3ujVXrKaiLD";

                MailMessage mess = new MailMessage
                {
                    From = new MailAddress(email, "This is test."),
                    Subject = "پاسخ",
                    IsBodyHtml = true,
                    Body = viewModel.Content
                };
                mess.To.Add(viewModel.MailTo);
                SmtpClient smtp = new SmtpClient
                {
                    Port = 587,
                    Host = "smtp.gmail.com",
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(email, pass),
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };
                smtp.Send(mess);

                if (!ModelState.IsValid) return BadRequest();
                var comment = _context.Comments.SingleOrDefault(c => c.ID == id);
                if (comment == null) return NotFound();

                comment.ReplyText = viewModel.Content;

                Log.Info($"Comment with id: {id} replied by admin with Username: {Identity.GetUsername()}");
                return Ok(_context.SaveChanges());
            }
            catch (Exception ex)
            {
                Log.Error($"Error while replying to Comment with id: {id}", ex);
                return BadRequest();
            }
        }
    }
}

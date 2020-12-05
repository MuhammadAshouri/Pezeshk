using AutoMapper;
using log4net;
using Pezeshk.Data;
using Pezeshk.Dtos;
using Pezeshk.Models;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Pezeshk.Controllers.Api
{
    [Authorize]
    public class PostController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(PostController));
        #region _context
        readonly ApplicationDbContext _context;
        public PostController() => _context = new ApplicationDbContext();
        protected override void Dispose(bool disposing) => _context.Dispose();
        #endregion

        [HttpPost]
        public IHttpActionResult AddPost(PostDto postDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var postUrl = $"/Content/Posts/{Date.GetDateTime().Replace(":", "-").Replace("/", "-")}.txt";
            File.WriteAllText(HttpContext.Current.Server.MapPath("~" + postUrl), postDto.Body);
            postDto.Body = postUrl;

            var post = Mapper.Map<PostDto, Post>(postDto);

            post.PostedOn = Date.GetDateTime();
            post.LastUpdate = post.PostedOn;

            _context.Posts.Add(post);
            _context.SaveChanges();

            Log.Info($"Post with id: {post.ID} added by admin with Username: {Identity.GetUsername()}");
            return Ok();
        }

        [HttpPut]
        public IHttpActionResult UpdatePost(int id, PostDto postDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var postInDb = _context.Posts.SingleOrDefault(c => c.ID == id);

            if (postInDb == null) return NotFound();

            File.WriteAllText(HttpContext.Current.Server.MapPath("~" + postInDb.Body), postDto.Body);
            postDto.Body = postInDb.Body;

            postDto.LastUpdate = Date.GetDateTime();
            postDto.PostedOn = postInDb.PostedOn;

            Mapper.Map(postDto, postInDb);

            _context.SaveChanges();
            Log.Info($"Post with id: {id} updated by admin with Username: {Identity.GetUsername()}");
            return Ok();
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetPosts() => Ok(_context.Posts.ToList().Select(Mapper.Map<Post, PostDto>));

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetPost(int id)
        {
            var post = _context.Posts.SingleOrDefault(c => c.ID == id);

            if (post == null) return NotFound();
            return Ok(Mapper.Map<Post, PostDto>(post));
        }

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetPostsForShow() =>
            Ok(_context.Posts.ToList().FindAll(m => m.Published == true).Select(Mapper.Map<Post, PostDto>));

        [HttpGet]
        [AllowAnonymous]
        public IHttpActionResult GetTopPosts(int count) =>
            Ok(_context.Posts.OrderByDescending(c => c.PostedOn).ToList().FindAll(m => m.Published == true)
                .Take(count).Select(Mapper.Map<Post, PostDto>));

        [HttpDelete]
        public IHttpActionResult DeletePost(int id)
        {
            var postInDb = _context.Posts.SingleOrDefault(c => c.ID == id);

            if (postInDb == null) return NotFound();

            File.Delete(HttpContext.Current.Server.MapPath("~" + postInDb.Body));

            _context.Posts.Remove(postInDb);
            _context.SaveChanges();
            Log.Info($"Post with id: {id} deleted by admin with Username: {Identity.GetUsername()}");
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult GetPostText(string path) =>
            Ok(File.ReadAllText(HttpContext.Current.Server.MapPath("~" + path)));
    }
}

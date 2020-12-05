using AutoMapper;
using log4net;
using PezeshkGit.Data;
using PezeshkGit.Dtos;
using PezeshkGit.Models;
using PezeshkGit.Models.Enum;
using PezeshkGit.Models.ViewModels;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace PezeshkGit.Controllers.Api
{
    [Authorize]
    public class AccountController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(AccountController));
        readonly ApplicationDbContext _context;
        public AccountController() => _context = new ApplicationDbContext();
        protected override void Dispose(bool disposing) => _context.Dispose();

        [HttpPost]
        public IHttpActionResult Register(AdminRegisterViewModel admin)
        {
            if (!ModelState.IsValid) return BadRequest();

            if (!Regex.IsMatch(admin.Username.Trim(), @"^(?=.{8,32}$)(?![_.])(?!.*[_.]{2})[a-zA-Z0-9._]+(?<![_.])$"))
                return BadRequest("Invalid Username");
            if (!Regex.IsMatch(admin.Password.Trim(), @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,32}$"))
                return BadRequest("Invalid Password");

            Admin admin1 = new Admin()
            {
                AddDate = Date.GetDateTime(),
                AdminType = admin.AdminType == "مدیر بلاگ" ?
                AdminType.BlogManager : AdminType.Secretary,
                Email = admin.Email.Trim(),
                Password = Hash.GetPasswordHash(admin.Password.Trim()),
                Username = admin.Username.Trim(),
                LastLogin = ""
            };

            _context.Admins.Add(admin1);
            _context.SaveChanges();

            Log.Info($"BlogManager added with Username: {admin1.Username}");

            return Ok(admin1);
        }

        [HttpPut]
        public IHttpActionResult UpdateAdmin(int id, AdminUpdateViewModel admin)
        {
            if (!ModelState.IsValid) return BadRequest();

            var adminInDb = _context.Admins.SingleOrDefault(c => c.ID == id);

            if (adminInDb == null) return NotFound();

            adminInDb.Username = admin.Username.Trim();
            adminInDb.Email = admin.Email.Trim();
            adminInDb.AdminType = admin.AdminType == "مدیر بلاگ" ?
                AdminType.BlogManager : AdminType.Secretary;

            _context.SaveChanges();

            Log.Info($"Admin with Username: {adminInDb.Username} and AdminType: {adminInDb.AdminType} altered!");
            return Ok(adminInDb);
        }

        [HttpGet]
        public IHttpActionResult GetAdmins() =>
            Ok(_context.Admins.ToList().Select(Mapper.Map<Admin, AdminDto>)
                .Where(c => c.AdminType != AdminType.FullAdmin));

        [HttpGet]
        public IHttpActionResult GetAdmin(int id)
        {
            var admin = _context.Admins.SingleOrDefault(c => c.ID == id);

            if (admin == null) return NotFound();
            return Ok(Mapper.Map<Admin, AdminDto>(admin));
        }

        [HttpDelete]
        public IHttpActionResult RemoveAdmin(int id)
        {
            var adminInDb = _context.Admins.SingleOrDefault(c => c.ID == id);

            if (adminInDb == null) return NotFound();

            _context.Admins.Remove(adminInDb);
            _context.SaveChanges();
            Log.Info($"Admin with Username: {adminInDb.Username} and AdminType: {adminInDb.AdminType} removed!");
            return Ok();
        }
    }
}

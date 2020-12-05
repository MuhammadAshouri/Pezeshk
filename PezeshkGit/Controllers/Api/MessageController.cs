using AutoMapper;
using log4net;
using Pezeshk.Data;
using Pezeshk.Dtos;
using Pezeshk.Models;
using Pezeshk.Models.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Http;

namespace Pezeshk.Controllers.Api
{
    [Authorize]
    public class MessageController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MessageController));
        #region _context
        readonly ApplicationDbContext _context;
        public MessageController() => _context = new ApplicationDbContext();
        protected override void Dispose(bool disposing) => _context.Dispose();
        #endregion

        public IHttpActionResult GetMessages() => Ok(_context.Messages.ToList().Select(Mapper.Map<Message, MessageDto>));

        public IHttpActionResult GetUnreadMessages() =>
            Ok(_context.Messages.Where(c => c.ReadState == false).ToList().Select(Mapper.Map<Message, MessageDto>));

        public IHttpActionResult GetUnreadMessagesCount() =>
            Ok(_context.Messages.Where(c => c.ReadState == false).ToList().Count);

        public IHttpActionResult GetMessage(int id)
        {
            var message = _context.Messages.SingleOrDefault(c => c.ID == id);

            if (message == null) return NotFound();
            return Ok(Mapper.Map<Message, MessageDto>(message));
        }

        [HttpPut]
        public IHttpActionResult UpdateReadState(int id)
        {
            if (!ModelState.IsValid) return BadRequest();
            var message = _context.Messages.SingleOrDefault(c => c.ID == id);
            if (message == null) return NotFound();
            message.ReadState = true;

            Log.Info($"Message with id: {id} readed by admin with Username: {Identity.GetUsername()}");
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
                var message = _context.Messages.SingleOrDefault(c => c.ID == id);
                if (message == null) return NotFound();

                message.ReplyText = viewModel.Content;

                Log.Info($"Message with id: {id} replied by admin with Username: {Identity.GetUsername()}");
                return Ok(_context.SaveChanges());
            }
            catch (Exception ex)
            {
                Log.Error($"Error while replying to Message with id: {id}", ex);
                return BadRequest();
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IHttpActionResult AddMessage(MessageDto messageDto)
        {
            if (!ModelState.IsValid) return BadRequest();

            var message = Mapper.Map<MessageDto, Message>(messageDto);

            message.ReplyText = "";
            message.Time = Date.GetDateTime();

            _context.Messages.Add(message);
            _context.SaveChanges();

            return Ok(messageDto);
        }
    }
}

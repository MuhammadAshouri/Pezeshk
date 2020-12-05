using System.ComponentModel;

namespace PezeshkGit.Models
{
    public class Message
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Time { get; set; }

        [DefaultValue(false)]
        public bool ReadState { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ReplyText { get; set; }
    }
}
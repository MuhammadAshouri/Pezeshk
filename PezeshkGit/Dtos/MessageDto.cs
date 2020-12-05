using System.ComponentModel;

namespace Pezeshk.Dtos
{
    public class MessageDto
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
using System.ComponentModel;

namespace PezeshkGit.Dtos
{
    public class CommentDto
    {
        public int ID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Time { get; set; }

        [DefaultValue(false)]
        public bool ReadState { get; set; }
        public string ReplyText { get; set; }
        public int Post_ID { get; set; }

        [DefaultValue(false)]
        public bool ShowInSite { get; set; }
    }
}
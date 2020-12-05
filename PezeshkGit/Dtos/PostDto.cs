using System.Collections.Generic;
using System.Web.Mvc;

namespace Pezeshk.Dtos
{
    public class PostDto
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Categories { get; set; }
        public string Tags { get; set; }

        [AllowHtml]
        public string Body { get; set; }
        public string PostedOn { get; set; }
        public bool Published { get; set; }
        public string LastUpdate { get; set; }
        public IList<CommentDto> Comments { get; set; }
    }
}
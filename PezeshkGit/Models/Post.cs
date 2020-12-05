using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Pezeshk.Models
{
    public class Post
    {
        public int ID { get; set; }

        [Display(Name = "عنوان")]
        [StringLength(100)]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [StringLength(300)]
        public string Description { get; set; }

        [AllowHtml]
        [Display(Name = "متن")]
        public string Body { get; set; }

        [Display(Name = "دسته بندی ها")]
        public string Categories { get; set; }

        [Display(Name = "برچسب ها")]
        public string Tags { get; set; }
        public string PostedOn { get; set; }

        [Display(Name = "نمایش در سایت")]
        public bool Published { get; set; }
        public string LastUpdate { get; set; }
        public IList<Comment> Comments { get; set; }
    }
}
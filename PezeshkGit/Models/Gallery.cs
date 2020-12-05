using System.ComponentModel.DataAnnotations;

namespace Pezeshk.Models
{
    public class Gallery
    {
        public int ID { get; set; }

        [Display(Name = "عنوان")]
        [StringLength(100)]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [StringLength(300)]
        public string Description { get; set; }
        public string URL { get; set; }

        [Display(Name = "دسته بندی ها")]
        public string Categories { get; set; }
        public string AddDate { get; set; }
        public string LastUpdate { get; set; }
    }
}
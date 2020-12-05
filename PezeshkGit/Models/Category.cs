using System.ComponentModel.DataAnnotations;

namespace PezeshkGit.Models
{
    public class Category
    {
        public int ID { get; set; }

        [Display(Name = "عنوان")]
        [StringLength(50)]
        public string Title { get; set; }

        [Display(Name = "تاریخ ثبت")]
        public string AddDate { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace PezeshkGit.Dtos
{
    public class CategoryDto
    {
        public int ID { get; set; }
        [StringLength(50)]
        public string Title { get; set; }
        public string AddDate { get; set; }
    }
}
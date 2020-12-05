using Pezeshk.Models.Enum;
using System.ComponentModel.DataAnnotations;

namespace Pezeshk.Dtos
{
    public class AdminDto
    {
        public int ID { get; set; }
        public string Username { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string Email { get; set; }
        public AdminType AdminType { get; set; }
        public string AddDate { get; set; }
        public string LastLogin { get; set; }
    }
}
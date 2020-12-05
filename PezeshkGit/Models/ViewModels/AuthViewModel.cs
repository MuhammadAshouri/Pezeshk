using PezeshkGit.Models.Enum;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PezeshkGit.Models.ViewModels
{
    public class LoginViewModel
    {
        [Display(Name = "نام کاربری")]
        public string Username { get; set; }
        [Display(Name = "گذرواژه")]
        public string Password { get; set; }
    }

    public class RegisterViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public AdminType AdminType { get; set; }
        public string AddDate { get; set; }
        public string LastLogin { get; set; }
        [DefaultValue("")]
        public string Email { get; set; }
    }
}
namespace Pezeshk.Models.ViewModels
{
    public class AdminUpdateViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string AdminType { get; set; }
    }

    public class AdminRegisterViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string AdminType { get; set; }
        public string Password { get; set; }
    }
}
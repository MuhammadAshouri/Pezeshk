using PezeshkGit.Models;
using PezeshkGit.Models.Enum;

namespace PezeshkGit
{
    public class AppUserState
    {
        public int UserId = 0;
        public string Username = "";
        public string Email = "";
        public AdminType AdminType = AdminType.Secretary;
        public string AddDate = "";
        public string LastLogin = "";

        /// <summary>
        /// Exports a short string list of Id, Username, Email, AdminType, AddDate, LastLogin separated by |
        /// </summary>
        /// <returns></returns>
        public override string ToString() =>
            string.Join("|", new string[] { UserId.ToString(), Username, Email, AdminType.ToString(), AddDate, LastLogin });

        /// <summary>
        /// Populates the AppUserState properties from a
        /// User instance
        /// </summary>
        /// <param name="user"></param>
        public void FromUser(Admin user)
        {
            UserId = user.ID;
            Username = user.Username;
            Email = user.Email;
            AdminType = user.AdminType;
            AddDate = user.AddDate;
            LastLogin = user.LastLogin;
        }

        /// <summary>
        /// Determines if the user is logged in
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty() => UserId == 0 || string.IsNullOrEmpty(Username);
    }
}
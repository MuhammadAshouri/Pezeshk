using Pezeshk.Models.Enum;
using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Pezeshk
{
    public class Date
    {
        public static string GetDateTime()
        {
            DateTime miladi = DateTime.Today;
            PersianCalendar shamsi = new PersianCalendar();

            string y = shamsi.GetYear(miladi).ToString();
            string m = shamsi.GetMonth(miladi).ToString();
            string d = shamsi.GetDayOfMonth(miladi).ToString();

            m = m.Length == 1 ? "0" + m : m;
            d = d.Length == 1 ? "0" + d : d;

            string sha = $"{y}/{m}/{d} {DateTime.Now.TimeOfDay.ToString().Split('.')[0]}";

            return sha;
        }

        public static DateTime ConvertToDateTime(string inp)
        {
            PersianCalendar shamsi = new PersianCalendar();

            var date = inp.Split(' ')[0].Split('/');
            int y = Convert.ToInt32(date[0]);
            int m = Convert.ToInt32(date[1]);
            int d = Convert.ToInt32(date[2]);

            var time = inp.Split(' ')[1].Split(':');
            int h = Convert.ToInt32(time[0]);
            int min = Convert.ToInt32(time[1]);
            int s = Convert.ToInt32(time[2]);

            return shamsi.ToDateTime(y, m, d, h, min, s, 0);
        }

        public static int GetYear() => new PersianCalendar().GetYear(DateTime.Today);

        public static string GetMonth()
        {
            var mon = new PersianCalendar().GetMonth(DateTime.Today).ToString();

            mon = mon.Length == 1 ? "0" + mon : mon;

            return mon;
        }
    }

    public class Hash
    {
        public static string GetPasswordHash(string str) =>
            Encoding.ASCII.GetString(new SHA256CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(str)));
    }

    public class Identity
    {
        public static bool IsAuthenticated() =>
            HttpContext.Current.GetOwinContext().Authentication.User.Identity.IsAuthenticated;

        public static string GetUsername() =>
            HttpContext.Current.GetOwinContext().Authentication.User.Identity.Name;

        public static AdminType GetAdminType()
        {
            var type = HttpContext.Current.GetOwinContext().Authentication.User.Claims
                   .SingleOrDefault(c => c.Type == "userState").Value.Split('|')[3];
            return (AdminType)Enum.Parse(typeof(AdminType), type);
        }

        public static bool IsInRole(AdminType type) => type == GetAdminType();
    }
}
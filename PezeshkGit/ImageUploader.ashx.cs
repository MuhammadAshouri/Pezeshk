using System;
using System.Drawing;
using System.IO;
using System.Web;

namespace PezeshkGit
{
    /// <summary>
    /// Summary description for ImageUploader
    /// </summary>
    public class ImageUploader : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            try
            {
                string str_image = "";
                CheckFolder();

                foreach (string s in context.Request.Files)
                {
                    HttpPostedFile file = context.Request.Files[s];
                    string fileName = file.FileName;
                    string fileExtension = file.ContentType;

                    if (!string.IsNullOrEmpty(fileName))
                    {
                        string fileExt = Path.GetExtension(fileName);
                        if (fileExt == ".jpg" || fileExt == ".jpeg" || fileExt == ".png")
                        {
                            var dt = DateTime.Now;
                            var date = $"{dt.Day}_{dt.Hour}_{dt.Minute}_{dt.Second}_";
                            string fName = $"{Date.GetYear()}/{Date.GetMonth()}/{date + Path.GetFileName(file.FileName)}";
                            file.SaveAs(MapIt("Full", fName));

                            // Resize For Thumb
                            Image img = Image.FromStream(file.InputStream);
                            Bitmap bit = ResizeImage(img, 200);
                            Bitmap bit2 = ResizeImage(img, 50);

                            bit.Save(MapIt("Thumb/200", fName));
                            bit2.Save(MapIt("Thumb/50", fName));

                            str_image = fName;
                        }
                        else str_image = "Invalid Format...";
                    }
                }
                context.Response.Write(str_image);
            }
            catch (Exception ex) { context.Response.Write(ex.Message); }
        }


        static string MapIt(string folder, string name) =>
            HttpContext.Current.Server.MapPath($"~/Content/Images/Uploads/{folder}/{name}");

        static void CheckFolder()
        {
            var year = Date.GetYear().ToString();
            var month = Date.GetMonth();

            if (!Directory.Exists(MapIt("Full", year))) Directory.CreateDirectory(MapIt("Full", year));
            if (!Directory.Exists(MapIt("Full", year + "/" + month))) Directory.CreateDirectory(MapIt("Full", year + "/" + month));
            if (!Directory.Exists(MapIt("Thumb/200", year))) Directory.CreateDirectory(MapIt("Thumb/200", year));
            if (!Directory.Exists(MapIt("Thumb/200", year + "/" + month))) Directory.CreateDirectory(MapIt("Thumb/200", year + "/" + month));
            if (!Directory.Exists(MapIt("Thumb/500", year))) Directory.CreateDirectory(MapIt("Thumb/50", year));
            if (!Directory.Exists(MapIt("Thumb/500", year + "/" + month))) Directory.CreateDirectory(MapIt("Thumb/50", year + "/" + month));
        }

        static Bitmap ResizeImage(Image img, int width)
        {
            float asp = (float)img.Width / img.Height;
            int height = Convert.ToInt32(width / asp);

            Bitmap nb = new Bitmap(width, height);
            Graphics gr = Graphics.FromImage(nb);

            gr.DrawImage(img, 0, 0, width, height);
            return nb;
        }

        public bool IsReusable => false;
    }
}
using System.Web;
using System.IO;


namespace Democracy.Classes
{
    public class Utilities
    {
        public static void UploadPhoto(HttpPostedFileBase file)
        {
            ////Upload Image:
            //string path = string.Empty;
            //string picture = string.Empty;

            //if (file != null)
            //{
            //    picture = Path.GetFileName(file.FileName);
            //    path = Path.Combine(Server.MapPath("~/Content/Photos"), picture);
            //    file.SaveAs(path);

            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        file.InputStream.CopyTo(ms);
            //        byte[] array = ms.GetBuffer();
            //    }
            //}
        }
    }
}
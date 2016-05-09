using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DA.AppBanwao.TariffBazaar.Helpers
{
    public class UploadHelper
    {
        public static string UploadFile(HttpPostedFileBase file, string uploadDirectory)
        {
            if (file.ContentLength > 0)
            {
                var fileName = Path.GetFileName(file.FileName);
                string path = Path.Combine(HttpContext.Current.Server.MapPath("~/" + uploadDirectory), fileName);
                file.SaveAs(path);
                return "~/" + uploadDirectory + "/" + fileName;
            }

            return string.Empty;
        }
    }
}
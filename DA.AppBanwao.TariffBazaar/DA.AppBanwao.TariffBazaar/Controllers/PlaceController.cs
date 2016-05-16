using DA.AppBanwao.TariffBazaar.Helpers;
using DA.AppBanwao.TariffBazaar.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace DA.AppBanwao.TariffBazaar.Controllers
{
    public class PlaceController : BaseController
    {
        //
        // GET: /Place/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult ContactDetails()
        {

            return View();
        }

        public ActionResult OtherDetails()
        {
            return View();
        }

        public ActionResult AddImages()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddImages(PlaceImageModel model)
        {
            string filePath = "";
            if (model.files.Count > 0)
            {
                foreach (var file in model.files)
                {
                    filePath += ((file != null) ? UploadHelper.UploadFile(file, "ItemImages") : string.Empty) + ";";
                }
            }
            string apiLink = ConfigurationManager.AppSettings["EventorsWebApiLink"].ToString() + "Place/SaveImages";
            string  appParameters = null;

            appParameters = "PlaceID=" + model.PlaceID + "&ImageLink=" + filePath;
            
            using (WebClient wc = new WebClient())
            {
                wc.Headers[HttpRequestHeader.ContentType] = "application/x-www-form-urlencoded";

                string result = wc.UploadString(apiLink, appParameters);
                
                if(result=="true")
                Success("<i class='fa fa-check'> </i> The venue is added successfully.",true);
                return RedirectToAction("Index", "Place");
            }
            return View();
        }
    }
}

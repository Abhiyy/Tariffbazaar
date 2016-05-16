using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA.AppBanwao.TariffBazaar.ViewModels
{
    public class PlaceImageModel
    {
        public Guid PlaceID { get; set; }

        public List<HttpPostedFileBase> files { get; set; }
    }
}
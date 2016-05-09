using DA.AppBanwao.TariffBazaar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA.AppBanwao.TariffBazaar.ViewModels
{
    public class UserDetailsModel:UserName
    {
        public int UserDetailsID { get; set; }

        public string Gender { get; set; }

        public DateTime DOB { get; set; }

        public string Profession { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Pincode { get; set; }

    }
}
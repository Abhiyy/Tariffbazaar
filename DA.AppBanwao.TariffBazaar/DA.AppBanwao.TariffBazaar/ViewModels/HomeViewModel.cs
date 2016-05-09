using DA.AppBanwao.TariffBazaar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA.AppBanwao.TariffBazaar.ViewModels
{
    public class HomeViewModel
    {
        public HomeViewModel()
        {
            Cities = new List<City>();
        }
        public List<City> Cities { get; set; }
        
    }
}
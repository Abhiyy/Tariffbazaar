using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DA.AppBanwao.TariffBazaar.ViewModels
{
    public class SliderModel
    {
        
        public int SliderID { get; set; }

        [Display(Name = "Title")]
        public string OfferHeading { get; set; }

        public string ImageLink { get; set; }

        [Display(Name = "Slider Image")]
        public HttpPostedFileBase SliderImage { get; set; }
        
        [Display(Name = "Offer Details")]
        public string Description { get; set; }

        [Display(Name = "Slider Order")]
        public int Order { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }
    }
}
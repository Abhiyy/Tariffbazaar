using DA.AppBanwao.TariffBazaar.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DA.AppBanwao.TariffBazaar.ViewModels
{
    public class RentItemDetailsModel:Item
    {
        [Display(Name="Category")]
        public string CategoryName { get; set; }
        [Display(Name = "Rent Criteria")]
        public string RentCriteriaName { get; set; }
        [Display(Name = "Ad Request Approved")]
        public string AdApprove { get; set; }
        [Display(Name = "Active")]
        public string AdActive { get; set; }
        [Display(Name = "Owner")]
        public string Owner { get; set; }
        [Display(Name = "Owner Contact")]
        public string Contact { get; set; }
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Display(Name = "City")]
        public string City { get; set; }
       
        [Display(Name = "Upload Photo 1")]
        public HttpPostedFileBase Image1 { get; set; }

        [Display(Name = "Upload Photo 2")]
        public HttpPostedFileBase Image2 { get; set; }

        [Display(Name = "Upload Photo 2")]
        public HttpPostedFileBase Image3 { get; set; }
      
        public bool ItemShipable { get; set; }

        [Display(Name = "Item on rent")]
        public bool IsRentable { get; set; }

        [Display(Name = "Item on sale")]
        public bool IsSellable { get; set; }

        [Display(Name = "Selling Price")]
        public double SellingPrice { get; set; }
    }
}
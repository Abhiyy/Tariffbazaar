using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DA.AppBanwao.TariffBazaar.ViewModels
{
    public class CreateRentItemModel
    {
        public int ItemId { get; set; }
        [Required]
        [Display(Name = "Item name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Category")]
        public int Category { get; set; }

        [Required]
        [Display(Name = "Add Features")]
        public string Features { get; set; }

        [Required]
        [Display(Name = "Terms and Conditions")]
        public string TermsConditions { get; set; }

        
        [Display(Name = "Min Days to be rented")]
        public int MinDays { get; set; }

        
        [Display(Name = "Rent")]
        public double Rent { get; set; }

        [Display(Name = "Security Deposit")]
        public double Security { get; set; }

        [Display(Name = "Your City")]
        public int City { get; set; }

        public int RentCriteria { get; set; }
        [Required]
        [Display(Name = "Upload Photo 1")]
        public HttpPostedFileBase Image1 { get; set; }

        [Display(Name = "Upload Photo 2")]
        public HttpPostedFileBase Image2 { get; set; }

        [Display(Name = "Upload Photo 2")]
        public HttpPostedFileBase Image3 { get; set; }

        public bool Shipable { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; }

        [Display(Name = "Approved")]
        public bool Approved { get; set; }

         [Display(Name = "Item on rent")]
        public bool Forrent { get; set; }

         [Display(Name = "Item on sale")]
        public bool ForSale { get; set; }
        
        [Display(Name = "Selling Price")]
        public double SaleAmount { get; set; }

    }
}
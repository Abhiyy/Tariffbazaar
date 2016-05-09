using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DA.AppBanwao.TariffBazaar.ViewModels
{
    public class LoginModel
    {
        [Required]
        [Display(Name="Email")]
        public string Email{get;set;}
        [Display(Name="Password")]
        [Required]
        public string Password { get; set; }
    }
}
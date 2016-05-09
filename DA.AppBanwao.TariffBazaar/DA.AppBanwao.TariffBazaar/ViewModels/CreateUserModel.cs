using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DA.AppBanwao.TariffBazaar.ViewModels
{
    public class CreateUserModel
    {
        public CreateUserModel()
        {
            this.CreatedOn = DateTime.UtcNow;
            this.UpdatedOn = DateTime.UtcNow;
        }

        [Required]
        [EmailAddress]
        [Display(Name="Email")]
        public string Email { get; set; }
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        [Display(Name = "User Name")]
        public string Username { get; set; }
        [Display(Name = "Password")]
        public string Password { get; set; }
        [Display(Name = "Active")]
        public bool Active { get; set; }
        [Display(Name = "User Type")]
        public string Role { get; set; }
        public DateTime CreatedOn { get; set; }

        public DateTime UpdatedOn { get; set; }
    }
}
using DA.AppBanwao.TariffBazaar.Helpers;
using DA.AppBanwao.TariffBazaar.Models;
using DA.AppBanwao.TariffBazaar.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA.AppBanwao.TariffBazaar.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        TariffBazaarEntities context = null;
        public ActionResult Index()
        {
            int itemLimit =Convert.ToInt32( ConfigurationManager.AppSettings["topitems"].ToString());
            using (context = new TariffBazaarEntities())
            {
                var topitems = context.Items.OrderByDescending(x=>x.UpdatedOn).Take(itemLimit).Select(s => s.RentItemId).ToList();
                var lstItems = GetItems(context, topitems);
                return View(lstItems.Where(x=>x.Active.Value && x.Approved.Value).ToList());
            }
            return View();
        }

        public ActionResult EarnRent()
        {
            return View();
        }

        public ActionResult ContactUs()
        {

            return View();

        }

        public ActionResult Termsofuse()
        {

            return View();
        }

        public ActionResult PrivacyPolicy()
        {

            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult FAQ()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult SendMessage(string User, string Email, string Message)
        {
            if(EmailHelper.SendFeedBackMessage(User, Email, Message))
                return Json(new { msg = "ok", url = Request.UrlReferrer.OriginalString.ToString() });
            else
                return Json(new { msg = "There is some error occured while sending the email, please try again.", url = Request.UrlReferrer.OriginalString.ToString() });
            return View();
        }


        List<RentItemDetailsModel> GetItems(TariffBazaarEntities context, List<int> items)
        {
            List<RentItemDetailsModel> lstItems = new List<RentItemDetailsModel>();
            foreach (var itemId in items)
            {
                lstItems.Add(GetItemDetails(itemId));
            }
            return lstItems;
        }

        RentItemDetailsModel GetItemDetails(int id)
        {
            RentItemDetailsModel objItem = new RentItemDetailsModel();
            using (context = new TariffBazaarEntities())
            {
                var objrentItem = context.Items.Find(id);
                objItem.Active = objrentItem.Active;
                objItem.Approved = objrentItem.Approved;
                objItem.CategoryId = objrentItem.CategoryId;
                objItem.CategoryName = context.Categories.Where(x => x.CategoryId == objrentItem.CategoryId).FirstOrDefault().Description.ToString();
                objItem.CreatedOn = objrentItem.CreatedOn;
                objItem.Description = objrentItem.Description;
                objItem.Features = objrentItem.Features;
                objItem.ImageLink1 = objrentItem.ImageLink1;
                objItem.ImageLink2 = objrentItem.ImageLink2;
                objItem.ImageLink3 = objrentItem.ImageLink3;
                objItem.MinimumDays = objrentItem.MinimumDays;
                objItem.Name = objrentItem.Name;
                objItem.Rent = objrentItem.Rent;
                objItem.RentCriteria = objrentItem.RentCriteria;
                objItem.RentCriteriaName = context.RentBasis.Where(x => x.RentBasisId == objItem.RentCriteria).FirstOrDefault().Description.ToString();
                objItem.RentItemId = objrentItem.RentItemId;
                objItem.SecurityAmount = objrentItem.SecurityAmount;
                objItem.Shipable = objrentItem.Shipable;
                objItem.TermsConditions = objrentItem.TermsConditions;
                objItem.UpdatedOn = objrentItem.UpdatedOn;

                objItem.City = context.Cities.Where(x => x.CityId == objrentItem.CityId).First().Description;
                objItem.AdActive = objItem.Active.Value ? "Yes" : "No";
                objItem.AdApprove = objItem.Approved.Value ? "Yes" : "No";
                var ownerdetails = context.UserNames.Where(x => x.UserId == objrentItem.UserId.Value);
                objItem.Email = ownerdetails.FirstOrDefault().EmailAddress;
                objItem.Contact = ownerdetails.FirstOrDefault().Phone;
                objItem.UserName = ownerdetails.FirstOrDefault().Usern;

            }
            return objItem;
        }
    }
}

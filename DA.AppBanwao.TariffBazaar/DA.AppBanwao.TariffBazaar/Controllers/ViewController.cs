using DA.AppBanwao.TariffBazaar.Helpers;
using DA.AppBanwao.TariffBazaar.Models;
using DA.AppBanwao.TariffBazaar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA.AppBanwao.TariffBazaar.Controllers
{
    [HandleError]
    public class ViewController : Controller
    {
        //
        // GET: /View/
        TariffBazaarEntities context = null;
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CitySearch(int id)
        {
            List<RentItemDetailsModel> lstItems = null;
            using (context = new TariffBazaarEntities())
            {
                lstItems = GetItemsByCity(context, id);
                    
            }
            return View(lstItems);
        }

        public ActionResult Category(int id)
        {
            List<RentItemDetailsModel> lstItems = null;
            using (context = new TariffBazaarEntities())
            {
                lstItems = GetItemsByCategory (context, id);
            }
            return View(lstItems);
        }

        public ActionResult Product(int id)
        {
            var itemDetails = GetItemDetails(id);
            return View(itemDetails);
        }

        List<RentItemDetailsModel> GetItemsByCity(TariffBazaarEntities context, int id)
        {
            var itemsByCategory = context.Items.Where(x => x.CityId == id).Select(s => s.RentItemId).ToList();
            List<RentItemDetailsModel> lstItems = new List<RentItemDetailsModel>();
            foreach (var itemId in itemsByCategory)
            { 
            lstItems.Add(GetItemDetails(itemId));
            }
            return lstItems.OrderByDescending(x => x.UpdatedOn).ToList();
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
                objItem.ForSale = objrentItem.ForSale.HasValue?objrentItem.ForSale.Value:false;
                objItem.ForRent = objrentItem.ForRent.Value;
                objItem.SaleAmount = objrentItem.SaleAmount.HasValue?objrentItem.SaleAmount.Value:0;

            }
            return objItem;
        }

        [HttpPost]
        public ActionResult SendMessage(int itemId, int period, string Message)
        {
            var item = GetItemDetails(itemId);
            using (context = new TariffBazaarEntities())
            {
                var requestorDetails = context.UserNames.Where(x => x.EmailAddress == User.Identity.Name).ToList();
                EmailHelper.SendUserMessage(item, requestorDetails, period, Message);
                return Json(new { msg = "ok", Message = "Your request has been sent successfully.", url = Request.UrlReferrer.OriginalString.ToString() });
            }
            return View();
        }

        public ActionResult GetItemsForRent()
        {
            var term = Request["term"].ToString();
            context = new TariffBazaarEntities();

            var items = context.Items.Where(x => x.Name.Contains(term)).Select(x => x.Name).ToList();

                return Json(items, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SearchProducts( )
        {
            context = new TariffBazaarEntities();
            string item = Request["search"].ToString();
            var lstItems = GetItemsByName(context, item);
            return View(lstItems);
        }

        List<RentItemDetailsModel> GetItemsByName(TariffBazaarEntities context, string itemName)
        {
            var itemsByName = context.Items.Where(x => x.Name.Contains(itemName)).Select(s => s.RentItemId).ToList();
            List<RentItemDetailsModel> lstItems = new List<RentItemDetailsModel>();
            foreach (var itemId in itemsByName)
            {
                lstItems.Add(GetItemDetails(itemId));
            }
            return lstItems.OrderByDescending(x => x.UpdatedOn).ToList();
        }
        List<RentItemDetailsModel> GetItemsByCategory(TariffBazaarEntities context, int id)
        {
            var itemsByName = context.Items.Where(x => x.CategoryId == id).Select(s => s.RentItemId).ToList();
            List<RentItemDetailsModel> lstItems = new List<RentItemDetailsModel>();
            foreach (var itemId in itemsByName)
            {
                lstItems.Add(GetItemDetails(itemId));
            }
            return lstItems.OrderByDescending(x=>x.UpdatedOn).ToList();
        }
    
    }
}

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
    public class RentItemController : BaseController
    {
        //
        // GET: /RentItem/
        TariffBazaarEntities context = null;
        [Authorize(Roles = "Customer,Admin")]
        public ActionResult Index()
        {
            List<Item> listItems = null;
           // string user = "Admin";
            using (context = new TariffBazaarEntities())
            {
                if (User.IsInRole("Admin"))
                {
                    listItems = context.Items.OrderByDescending(x => x.UpdatedOn).ToList();
                }
                else
                {
                    listItems = context.Items.Where(x => x.UserId == context.UserNames.Where(u=>u.EmailAddress== User.Identity.Name).FirstOrDefault().UserId).OrderByDescending(x => x.UpdatedOn).ToList();
                }
            }
            return View(listItems);
        }
        [Authorize(Roles= "Customer,Admin")]
        public ActionResult UploadItem()
        {
            context = new TariffBazaarEntities();
                CreateViewBag(context);
          
            return View();
        }

        [Authorize(Roles = "Customer,Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadItem(CreateRentItemModel model)
        {
            using (context = new TariffBazaarEntities())
            {
                if (ModelState.IsValid)
                {
                    var objItem = GetItem(model);

                    context.Items.Add(objItem);
                    context.SaveChanges();
                    //Add message 
                    Success("<strong> The product has been added successfully. However, it will be public once, the details are verified by the administrator.</strong>", true);
                    //return Content("Your item has been successfully submitted for verification. Our representative will review it and will be on the website in the next 24 hours.");
                    return RedirectToAction("Index", "RentItem");


                }
                else
                {
                    CreateViewBag(context);
                    return View(model);
                }
            }
            return View();
        }
        [Authorize(Roles = "Customer,Admin")]
        public ActionResult ApproveRequest(int id)
        {
            using (context = new TariffBazaarEntities())
            {
                var rentItem = context.Items.Find(id);
                rentItem.Active = true;
                rentItem.Approved = true;
                context.Entry(rentItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                //Message for success
                Success("<strong> The product is approved successfully.</strong>", true);
                return RedirectToAction("Index", "RentItem");
            }
            return View();
        }
        [Authorize(Roles = "Customer,Admin")]
        public ActionResult ActivateDeactivate(int id)
        {
            using (context = new TariffBazaarEntities())
            {
                var rentItem = context.Items.Find(id);
                rentItem.Active = rentItem.Active.Value?false:true;
                string msg = rentItem.Active.Value?"<strong> The product is activated successfully.</strong>":"<strong> The product is de-activated successfully.</strong>";
                context.Entry(rentItem).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                //Message for success
                
                Success(msg, true);
                return RedirectToAction("Index", "RentItem");
            }
            return View();
        
        }
        [Authorize(Roles = "Customer,Admin")]
        public ActionResult Details(int id)
        {
           
            return View(GetItemDetails(id));
        }
        [Authorize(Roles = "Customer,Admin")]
        public ActionResult Edit(int id)
        {
            using (context = new TariffBazaarEntities())
            {
                CreateViewBag(context);
            }
            return View(GetItemDetails(id));
        }
        [Authorize(Roles = "Customer,Admin")]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(RentItemDetailsModel model)
        {
            using (context = new TariffBazaarEntities()) {
                var item = UpdateItem(context.Items.Find(model.RentItemId),model,context);
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                Success("<strong> The product details updated successfully.</strong>", true);
                return RedirectToAction("Index", "RentItem");
            }

            return View();
        }
        [Authorize(Roles = "Customer,Admin")]
        public ActionResult Delete(int id)
        {
            using (context = new TariffBazaarEntities())
            {
                var item = context.Items.Find(id);
                context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
                Success("<strong> The product is deleted successfully.</strong>", true);
                
                return RedirectToAction("Index", "RentItem");
            }
            return View();
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
                objItem.IsRentable = objrentItem.ForRent.HasValue?objrentItem.ForRent.Value:false;
                objItem.IsSellable = objrentItem.ForSale.HasValue ? objrentItem.ForSale.Value : false;
                objItem.SellingPrice = Convert.ToDouble(objrentItem.SaleAmount.Value);
                objItem.City = context.Cities.Where(x => x.CityId == objrentItem.CityId).First().Description ;
                objItem.AdActive = objItem.Active.Value ? "Yes" : "No";
                objItem.AdApprove = objItem.Approved.Value ? "Yes" : "No";
                var ownerdetails = context.UserNames.Where(x => x.UserId == objrentItem.UserId.Value);
                objItem.Email = ownerdetails.FirstOrDefault().EmailAddress;
                objItem.Contact = ownerdetails.FirstOrDefault().Phone;
                objItem.UserName = ownerdetails.FirstOrDefault().Usern;
                
            }
            return objItem;
        }
        
        Item GetItem(CreateRentItemModel model)
        {
            context = new TariffBazaarEntities();
            var objItem = new Item();
            objItem.Active = false;
            objItem.Approved = false;
            objItem.CategoryId = model.Category;
            objItem.CreatedOn = DateTime.UtcNow;
            objItem.Description = model.Description;
            objItem.Features = model.Features;
            objItem.ImageLink1 = UploadHelper.UploadFile(model.Image1, "ItemImages");
            objItem.ImageLink2 = (model.Image2 != null) ? UploadHelper.UploadFile(model.Image2, "ItemImages") : string.Empty;
            objItem.ImageLink3 = (model.Image3 != null) ? UploadHelper.UploadFile(model.Image3, "ItemImages") : string.Empty;
            objItem.MinimumDays = model.MinDays;
            objItem.Name = model.Name;
            if (model.Forrent)
            {
                objItem.ForRent = model.Forrent;
                objItem.Rent = Convert.ToDecimal(model.Rent);
                objItem.RentCriteria = model.RentCriteria;
                objItem.SecurityAmount = Convert.ToDecimal(model.Security);
            }
            objItem.Shipable = model.Shipable;
            objItem.TermsConditions = model.TermsConditions;
            objItem.UpdatedOn = DateTime.UtcNow;
            objItem.UserName = User.Identity.Name;
            objItem.CityId = model.City;
            
            if (model.ForSale)
            {
                objItem.ForSale = model.ForSale;
                objItem.SaleAmount = Convert.ToDecimal(model.SaleAmount);
            }
            objItem.UserId = context.UserNames.Where(x => x.EmailAddress == User.Identity.Name).FirstOrDefault().UserId;
            return objItem;
        }
        
        Item UpdateItem(Item ItemtoEdit, RentItemDetailsModel model, TariffBazaarEntities context)
        {
            ItemtoEdit.CityId = model.CityId;
            ItemtoEdit.Description = model.Description;
            ItemtoEdit.Features = model.Features;
            ItemtoEdit.ImageLink1 = (model.Image1 !=null)? UploadHelper.UploadFile(model.Image1, "ItemImages"):model.ImageLink1;
            ItemtoEdit.ImageLink2 = (model.Image2 != null) ? UploadHelper.UploadFile(model.Image2, "ItemImages") : model.ImageLink2;
            ItemtoEdit.ImageLink3 = (model.Image3 != null) ? UploadHelper.UploadFile(model.Image3, "ItemImages") : model.ImageLink3;
            ItemtoEdit.MinimumDays = model.MinimumDays;
            ItemtoEdit.Name = model.Name;
            ItemtoEdit.ForRent = model.IsSellable;
            ItemtoEdit.ForSale = model.IsRentable;
            ItemtoEdit.SaleAmount = Convert.ToDecimal( model.SellingPrice);
            ItemtoEdit.Rent = model.Rent;
            ItemtoEdit.RentCriteria = model.RentCriteria;
            ItemtoEdit.SecurityAmount = model.SecurityAmount;
            ItemtoEdit.Shipable = model.ItemShipable;
            ItemtoEdit.TermsConditions = model.TermsConditions;
            ItemtoEdit.UpdatedOn = DateTime.UtcNow;
            ItemtoEdit.UserName = model.Email;
            return ItemtoEdit;
        }
        
        void CreateViewBag(TariffBazaarEntities context) {

            context = new TariffBazaarEntities();
            ViewBag.Categories = new SelectList(context.Categories, "CategoryId", "Description");
            ViewBag.RentCriteria = new SelectList(context.RentBasis, "RentBasisId", "Description");
            ViewBag.Cities = new SelectList(context.Cities, "CityId", "Description");
            context = null;
        }



        
    }
}

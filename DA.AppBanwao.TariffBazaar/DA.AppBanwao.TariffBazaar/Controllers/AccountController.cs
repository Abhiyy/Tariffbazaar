using DA.AppBanwao.TariffBazaar.Helpers;
using DA.AppBanwao.TariffBazaar.Models;
using DA.AppBanwao.TariffBazaar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DA.AppBanwao.TariffBazaar.Controllers
{
    [HandleError]
    public class AccountController : BaseController
    {
        //
        // GET: /Account/
        TariffBazaarEntities _context = null;

        [Authorize(Roles="Admin")]
        public ActionResult Index()
        {
            _context = new TariffBazaarEntities();
            return View(GetUsers(_context));
        }


        public ActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel user, string returnUrl)
        {
            using (_context = new TariffBazaarEntities())
            {
                if (ModelState.IsValid)
                {
                    string encyrptedPass = EncryptionManager.EncryptionManager.ConvertToUnSecureString(EncryptionManager.EncryptionManager.EncryptData(user.Password));
                    if (_context.UserNames.Any(x => x.EmailAddress == user.Email && x.Password == encyrptedPass))
                    {
                        var userdetails = _context.UserNames.Where(x => x.EmailAddress == user.Email);


                        FormsAuthentication.SetAuthCookie(user.Email, false);
                        //if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/") && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                        //{
                        //    return Redirect(returnUrl);
                        //}
                        //else
                        //{
                        if (userdetails.FirstOrDefault().Role == "Customer")
                        {
                                return RedirectToAction("Index", "Home");
                        }
                        else if (userdetails.FirstOrDefault().Role == "WebAdmin" || userdetails.FirstOrDefault().Role == "Admin")
                            return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        //Add log for unsuccessful login
                        // Message for user
                        Danger("<strong>Invalid username/password. Please try again.</strong>",true);
                    }


                }
                else
                {
                    Danger("<strong>Invalid username/password. Please try again.</strong>", true);
                    return View(user);
                }
            }
            return View();
        }

        [HttpPost]
        public ActionResult LoginUser(string email, string pwd)
        {
            using (_context = new TariffBazaarEntities())
            {
                   string encyrptedPass = EncryptionManager.EncryptionManager.ConvertToUnSecureString(EncryptionManager.EncryptionManager.EncryptData(pwd));
                    if (_context.UserNames.Any(x => x.EmailAddress == email && x.Password == encyrptedPass))
                    {
                        var userdetails = _context.UserNames.Where(x => x.EmailAddress == email);


                        FormsAuthentication.SetAuthCookie(email, false);
                       
                        return Json(new { msg="ok",url=Request.UrlReferrer.OriginalString.ToString()});
                    }
                    else
                    {
                        //Add log for unsuccessful login
                        return Json(new { msg = "Invalid username/password, please try again.", url = Request.UrlReferrer.OriginalString.ToString() });
                    }


            }
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(SignUpModel guestdetails)
        {
            _context = new TariffBazaarEntities();
            if (ModelState.IsValid)
            {
                if (!_context.UserNames.Any(x => x.EmailAddress == guestdetails.Email))
                {
                    var newUser = new UserName();
                    newUser.EmailAddress = guestdetails.Email;
                    newUser.Password = EncryptionManager.EncryptionManager.ConvertToUnSecureString(EncryptionManager.EncryptionManager.EncryptData(guestdetails.Password));
                    newUser.Role = "Customer";
                    newUser.Usern = guestdetails.Name;
                    newUser.Active = false;
                    newUser.Phone = guestdetails.Phone;
                    newUser.CreatedOn = DateTime.Now;
                    newUser.UpdatedOn = DateTime.Now;
                    _context.UserNames.Add(newUser);
                    _context.SaveChanges();

                    //Send Confirmation on user email
                    EmailHelper.SendConfirmationEmail(newUser.EmailAddress, _context);
                 //   return Json(new { messagetype = "success", message = "", url = Url.Action("UserRegistered", "Account") });
                    return RedirectToAction("UserRegistered", "Account");
                }
                else
                {
                   // return RedirectToAction("Register", "Account");
                    Danger("<strong>Email - address already exists in our records. Please try with different email.</strong>", true);
                    return View(guestdetails);
                }
            }
            else
            {
                return View(guestdetails);
            }
            return View();
        }

        public ActionResult UserRegistered()
        {

            return View();
        }

        public ActionResult VerifyUser(string EmailAddress, string AuthCode)
        {
            _context = new TariffBazaarEntities();

            if (_context.OtpHolders.Any(x => x.OtpReference == EmailAddress && x.Otp == AuthCode))
            {
                var user = _context.UserNames.First(c => c.EmailAddress == EmailAddress);
                user.Active = true;
                _context.Entry(user).State =System.Data.Entity.EntityState.Modified;

                var otpdetails = _context.OtpHolders.First(c => c.OtpReference == EmailAddress);
                _context.OtpHolders.Remove(otpdetails);
                _context.SaveChanges();
                ViewBag.IsConfirmed = true;
            }
            ViewBag.User = EmailAddress;
            return View();
        }

        [Authorize(Roles = "Admin,Customer")]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult MyProfile()
        {
        return View();
        }
        
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            CreateViewBag();
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateUserModel model)
        {
            try
            {
                var user = new UserName();
                user.Active = model.Active;
                user.CreatedOn = model.CreatedOn;
                user.EmailAddress = model.Email;
                user.Password = EncryptionManager.EncryptionManager.ConvertToUnSecureString(EncryptionManager.EncryptionManager.EncryptData(model.Password));
                user.Phone = model.Phone;
                user.Role = model.Role;
                user.UpdatedOn = model.UpdatedOn;
                user.Usern = model.Username;

                _context = new TariffBazaarEntities();
                _context.UserNames.Add(user);
                _context.SaveChanges();
                var userDetails = new UserDetail();
                userDetails.UserId = user.UserId;
                _context.UserDetails.Add(userDetails);
                _context.SaveChanges();
                //_logger.WriteLog(CommonHelper.MessageType.SUCCESS,"User created successfully.","Create","AccountController","")
                return RedirectToAction("Index", "Account");
            }
            catch (Exception ex) { 
            
            }
            return View();
        }
        List<UserDetailsModel> GetUsers(TariffBazaarEntities context)
        {

            List<UserDetailsModel> lstUsers = new List<UserDetailsModel>();
            var users = context.UserNames.ToList();
            foreach (var user in users)
            {
                
                lstUsers.Add(GetUserDetails(user,_context.UserDetails.Where(x=>x.UserId == user.UserId).FirstOrDefault(),context));
            }
            return lstUsers;
        }

        

        UserDetailsModel GetUserDetails(UserName user, UserDetail otherUserDetails,TariffBazaarEntities context)
        {
            UserDetailsModel userDetail = new UserDetailsModel();

            userDetail.UserId = user.UserId;
            userDetail.Usern = user.Usern;
            userDetail.Active = user.Active;
            userDetail.CreatedOn = user.CreatedOn;
            userDetail.UpdatedOn = user.UpdatedOn;
            userDetail.Role = user.Role;
            userDetail.EmailAddress = user.EmailAddress;
            userDetail.Phone = user.Phone;
            if (otherUserDetails != null)
            {
                userDetail.City = otherUserDetails.CityId != null ? context.Cities.Find(otherUserDetails.CityId).Description : string.Empty;
                userDetail.Country = otherUserDetails.CountryId != null ? context.Countries.Find(otherUserDetails.CountryId).Name : string.Empty;
                userDetail.DOB = otherUserDetails.DOB != null ? otherUserDetails.DOB.Value : DateTime.Now;
                userDetail.Gender = otherUserDetails.Gender != null ? (otherUserDetails.Gender == 1) ? "Male" : "Female" : string.Empty;
                userDetail.Pincode = otherUserDetails.Pincode != null ? otherUserDetails.Pincode : string.Empty;
                userDetail.Profession = otherUserDetails.Profession != null ? otherUserDetails.Profession : string.Empty;
                userDetail.UserDetailsID = otherUserDetails.UserDetailsId;
            }
            return userDetail;
        }
        void CreateViewBag()
        {
            _context = new TariffBazaarEntities();

            ViewBag.Roles = new SelectList(_context.Roles, "RoleName", "RoleName");
            _context = null;
        }

    }
}

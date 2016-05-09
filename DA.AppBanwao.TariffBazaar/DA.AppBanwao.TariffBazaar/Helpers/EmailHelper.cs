using DA.AppBanwao.TariffBazaar.Models;
using DA.AppBanwao.TariffBazaar.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA.AppBanwao.TariffBazaar.Helpers
{
    public class EmailHelper
    {
        public static void SendConfirmationEmail(string emailaddress, TariffBazaarEntities context)
        {

            string otpCode = OTPManager.OTPGenerator.RandomNumber(100000, 999999).ToString() + OTPManager.OTPGenerator.RandomString(6, true);
            OtpHolder otpdetails = new OtpHolder();

            if (context.OtpHolders.Any(x => x.OtpReference == emailaddress))
            {
                otpdetails = context.OtpHolders.Find(emailaddress);
                otpdetails.Otp = otpCode;
                context.Entry(otpdetails).State=  System.Data.Entity.EntityState.Modified;

            }
            else
            {
                otpdetails.OtpReference = emailaddress;
                otpdetails.Otp = otpCode;
                context.OtpHolders.Add(otpdetails);
            }
            context.SaveChanges();
            string url = HttpContext.Current.Request.Url.AbsoluteUri.Replace("Register", string.Empty) + "VerifyUser?EmailAddress=" + emailaddress + "&AuthCode=" + otpCode;
            SendConfirmEmail(url, emailaddress, context);
        }

        static void SendConfirmEmail(string url, string emailAddress, TariffBazaarEntities context)
        {
            //StringBuilder
            EmailEngine.Mailer objConfirmationEmail = new EmailEngine.Mailer();
            objConfirmationEmail.Recipient = emailAddress;
            objConfirmationEmail.Body = GetConfirmationEmailBody(emailAddress, url);
            objConfirmationEmail.Sender = "support@tariffbazaar.com";
            objConfirmationEmail.Subject = "TariffBazaar.com: Confirmation-Email";
            objConfirmationEmail.Send();


        }

        static string GetConfirmationEmailBody(string emailAddress, string url)
        {
            string body = "Hello " + emailAddress.Trim() + ",";
            body += "<br /><br />Please click the following link to activate your account";
            body += "<br /><a href = '" + url + "'>Click here to activate your account.</a>";
            body += "<br /><br />Thanks,<br /> <b>TariffBazaar Team</b>";
            return body;
        }
        
        static string GetUserMessage(RentItemDetailsModel item, List<UserName> user, int rentPeriod, string message)
        {
            string body = "Hello " + item.UserName + ",";
            body += "<br /><br />A user has shown details in the item that you have added on tariffbazaar.com, below is the details: ";
            body += "<br /><br/>";
            body += "Product Name: "+item.Name+"<br />";
            body += "Product Description: " + item.Description + "<br />";
            body += "<br /><br/>";
            body += "Requestor Details <hr /> ";
            body += "Name: " + user.FirstOrDefault().Usern + "<br />";
            body += "Email: " + user.FirstOrDefault().EmailAddress + "<br />";
            body += "Contact: " + user.FirstOrDefault().Phone + "<br />";
            body += "Period of rent requested: " + rentPeriod.ToString() + "<br />";
            body += "Requestor Message: " + message + "<br />";
            body += "<br/>Please get in touch with the requestor, if you are interested, in giving the product on rent.";
            body += "<br /><br />Thanks,<br /> <b>TariffBazaar Team</b>";
            body += "<br /><br />This email has been sent because you are an registered user of <b>www.tariffbazaar.com</b> user. If you feel, this email should not reach to you please write to us on <a href='mailto:support@tariffbazaar.com'>support@tariffbazaar.com</a>.";
            return body;
        }

        static string GetFeedbackBody(string User, string Email, string Message)
        {
            string body = "Hello Admin,";
            body += "<br /><br />A user has sent a message, please find below details of the message: ";
            body += "<br /><br/>";
            body += "User Name: " + User + "<br />";
            body += "Email: " + Email + "<br />";
            body += "Message: " + Message + "<br />";
            body += "<br /><br/>";
            body += "<br/>Please get in touch with the user.";
            body += "<br /><br />Thanks,<br /> <b>TariffBazaar Team</b>";
            body += "<br /><br />This email has been sent because you are an registered user of <b>www.tariffbazaar.com</b> user. If you feel, this email should not reach to you please write to us on <a href='mailto:support@tariffbazaar.com'>support@tariffbazaar.com</a>.";
            return body;
        }

        static string RequestorAcknowledgementMessage(RentItemDetailsModel item, List<UserName> user, int rentPeriod,string message)
        {
            string body = "Hello " + user.FirstOrDefault().Usern + ",";
            body += "<br /><br />Many thanks for showing your interest in the item on tariffbazaar.com, we have sent your request to the owner, below is the details: ";
            body += "<br /><br/>";
            body += "Product Name: " + item.Name + "<br />";
            body += "Product Description: " + item.Description + "<br />";
            body += "Period of rent requested: " + rentPeriod.ToString() + "<br />";
            body += "Your message: " + message + "<br />";
            body += "<br /><br/>";
            body += "Owner Details <hr /> ";
            body += "Name: " + item.UserName + "<br />";
            body += "Email: " + item.Email + "<br />";
            body += "Contact: " + item.Contact + "<br />";
            
            body += "<br/>Please get in touch with the owner personally if you want interested or please have patience for the reply";
            body += "<br /><br />Thanks,<br /> <b>TariffBazaar Team</b>";
            body += "<br /><br />This email has been sent because you are an registered user of <b>www.tariffbazaar.com</b> user. If you feel, this email should not reach to you please write to us on <a href='mailto:support@tariffbazaar.com'>support@tariffbazaar.com</a>.";
            return body;
        }
        public static void SendResetPasswordEmail(string password, string email, string username)
        {
            string body = "Hello " + username.Trim() + ",";
            body += "<br /><br />Please find your details below:";
            body += "<br /><b>New Password: " + password;
            body += "<br/>Please change your password after login to your account.<br /><br />Thanks,<br /> <b>TariffBazaar Team</b>";

            EmailEngine.Mailer objPasswordResetEmail = new EmailEngine.Mailer();
            objPasswordResetEmail.Recipient = email;
            objPasswordResetEmail.Body = body;
            objPasswordResetEmail.Sender = "support@tariffbazaar.com";
            objPasswordResetEmail.Subject = "TariffBazaar: Password Reset-Email";
            objPasswordResetEmail.Send();
        }

        public static bool SendUserMessage(RentItemDetailsModel item, List<UserName> user, int rentPeriod, string message)
        {

            EmailEngine.Mailer objConfirmationEmail = new EmailEngine.Mailer();
            objConfirmationEmail.Recipient = item.Email;
            objConfirmationEmail.RecipientCC = "admin@tariffbazaar.com";
            objConfirmationEmail.Body = GetUserMessage(item,user,rentPeriod, message);
            objConfirmationEmail.Sender = "admin@tariffbazaar.com";
            objConfirmationEmail.Subject = "TariffBazaar.com: Rent request for your item - "+item.Name;
            objConfirmationEmail.Send();

            objConfirmationEmail.Recipient = user.FirstOrDefault().EmailAddress;
            objConfirmationEmail.RecipientCC = "admin@tariffbazaar.com";
            objConfirmationEmail.Body = RequestorAcknowledgementMessage(item, user, rentPeriod,message);
            objConfirmationEmail.Sender = "admin@tariffbazaar.com";
            objConfirmationEmail.Subject = "TariffBazaar.com: Acknowledgement for item request - " + item.Name;
            objConfirmationEmail.Send();

            return true;
        }

        public static bool SendFeedBackMessage(string User, string Email, string Message)
        {
            EmailEngine.Mailer objConfirmationEmail = new EmailEngine.Mailer();
            objConfirmationEmail.Recipient = "admin@tariffbazaar.com";
            objConfirmationEmail.RecipientCC = "abhi.recoil2051@gmail.com";
            objConfirmationEmail.Body = GetFeedbackBody(User, Email, Message);
            objConfirmationEmail.Sender = "admin@tariffbazaar.com";
            objConfirmationEmail.Subject = "TariffBazaar.com: User Contact - " + User;
            objConfirmationEmail.Send();

            objConfirmationEmail.Recipient = User;
           // objConfirmationEmail.RecipientCC = "abhi.recoil2051@gmail.com";
            objConfirmationEmail.Body = GetFeedbackAcknowlegmentBody(User);
            objConfirmationEmail.Sender = "admin@tariffbazaar.com";
            objConfirmationEmail.Subject = "TariffBazaar.com: Acknowledgement for Feedback submit";
            objConfirmationEmail.Send();

            return true;
        }
        static string GetFeedbackAcknowlegmentBody(string User)
        {
            string body = "Hello "+User+",";
            body += "<br /><br />Thank you for submitting your feedback, we will get back to you in next 48 hours, if necessary. ";
            body += "<br /><br />Thanks,<br /> <b>TariffBazaar Team</b>";
            return body;
        }

    }
}
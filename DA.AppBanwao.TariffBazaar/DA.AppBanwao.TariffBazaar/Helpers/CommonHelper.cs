using DA.AppBanwao.TariffBazaar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DA.AppBanwao.TariffBazaar.Helpers
{
    public class CommonHelper
    {
        public class Roles
        {
            public static string ADMIN = "Admin";
            public static string WEBADMIN = "WebAdmin";
            public static string WEBUSER = "WebUser";

        }

        public class MessageType
        {
            public static string INFORMATION = "Information";
            public static string SUCCESS = "Success";
            public static string ERROR = "Error";
            public static string EXCEPTION = "Exception";
            public static string WARNING = "Warning";
        }



        public class UserDetails
        {
            public static string GetIpAddress()
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
        }

        public class Logger
        {
            TariffBazaarEntities _context = null;

            public Logger()
            {

            }

            public void WriteLog(string MessageType, string Message, string Method, string FileName, string UserName)
            {
                var logger = new Log();

                logger.MessageType = MessageType;
                logger.Message = Message;
                logger.MethodName = Method;
                logger.FileName = FileName;
                logger.UserName = UserName;
                logger.IpAddress = UserDetails.GetIpAddress();
                logger.EventTimeStamp = DateTime.UtcNow;
                _context = new TariffBazaarEntities();

                _context.Logs.Add(logger);
                _context.SaveChanges();
                logger = null;
                _context = null;


            }
        }
    }
}
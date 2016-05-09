using DA.AppBanwao.TariffBazaar.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA.AppBanwao.TariffBazaar.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/
        public  DA.AppBanwao.TariffBazaar.Helpers.CommonHelper.Logger _logger = new CommonHelper.Logger();

        public void Success(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void Information(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
            var alerts = TempData.ContainsKey(MessageHelper.TempDataKey)
                ? (List<MessageHelper>)TempData[MessageHelper.TempDataKey]
                : new List<MessageHelper>();

            alerts.Add(new MessageHelper
            {
                AlertStyle = alertStyle,
                Message = message,
                Dismissable = dismissable
            });

            TempData[MessageHelper.TempDataKey] = alerts;
        }

    }
}

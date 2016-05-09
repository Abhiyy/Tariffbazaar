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
    public class SliderController : Controller
    {
        //
        // GET: /Slider/
        TariffBazaarEntities _context = null;
        string _sliderDirectory = ConfigurationManager.AppSettings["SliderDirectory"].ToString();

        public ActionResult Index()
        {
            _context = new TariffBazaarEntities();
            var lstSlider = _context.Sliders.ToList();
            return View(lstSlider);
        }

        [Authorize(Roles="Admin")]
        public ActionResult Create()
        {

            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SliderModel model)
        {
            _context = new TariffBazaarEntities();
            try
            {
                var slider = new Slider();
                slider.OfferHeading = model.OfferHeading;
                slider.Offer = model.Description;
                slider.ImageLink = UploadHelper.UploadFile(model.SliderImage,_sliderDirectory);
                int lastOrder = _context.Sliders.Count() > 0 ? _context.Sliders.OrderByDescending(x => x.SlideOrder).First().SlideOrder.Value : 0;
                slider.SlideOrder = ++lastOrder;
                slider.Active = model.Active;
                _context.Sliders.Add(slider);
                _context.SaveChanges();
                _context = null;
                return RedirectToAction("Index", "Slider");
            }
            catch (Exception ex) { 
            
            }

            return View();
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Edit()
        {

            return View();
        }
    }
}

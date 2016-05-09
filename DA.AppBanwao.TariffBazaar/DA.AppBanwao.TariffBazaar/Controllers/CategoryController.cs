using DA.AppBanwao.TariffBazaar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DA.AppBanwao.TariffBazaar.Controllers
{
    
    public class CategoryController : Controller
    {
        //
        // GET: /Categories/
        TariffBazaarEntities _context = null;
        public ActionResult Index()
        {
            _context = new TariffBazaarEntities();
            var lstCategories = _context.Categories.ToList();
            _context = null;
            return View(lstCategories);
        }
        [Authorize(Roles="Admin")]
        public ActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category model) 
        {
            _context = new TariffBazaarEntities();

            var objCategory = new Category();
            objCategory.Description = model.Description;
            _context.Categories.Add(objCategory);
            _context.SaveChanges();
            _context = null;
            return View();
        }
    }
}

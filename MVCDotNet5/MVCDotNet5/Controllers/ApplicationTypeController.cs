using Microsoft.AspNetCore.Mvc;
using MVCDotNet5.Data;
using MVCDotNet5.Models;
using System.Collections.Generic;

namespace MVCDotNet5.Controllers
{
    public class ApplicationTypeController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ApplicationTypeController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<ApplicationType> appType = _db.ApplicationTypes;
            return View(appType);
        }

        //GET -- CREATE
        public IActionResult Create()
        {
            return View();
        }

        //POST -- CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ApplicationType appType)
        {
            _db.ApplicationTypes.Add(appType);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

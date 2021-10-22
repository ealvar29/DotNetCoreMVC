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
            if (ModelState.IsValid)
            {
                _db.ApplicationTypes.Add(appType);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appType);
        }

        //GET -- EDIT
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.ApplicationTypes.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        //POST -- EDIT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ApplicationType appType)
        {
            if (ModelState.IsValid)
            {
                _db.ApplicationTypes.Update(appType);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(appType);
        }

        //GET -- DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.ApplicationTypes.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        //POST -- DELETE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _db.ApplicationTypes.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.ApplicationTypes.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

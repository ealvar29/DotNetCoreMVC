using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCDotNet5.Data;
using MVCDotNet5.Models;
using MVCDotNet5.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MVCDotNet5.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ProductController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _db.Products;
            foreach(var obj in products)
            {
                obj.Category = _db.Categories.FirstOrDefault(x => x.Id == obj.CategoryId);
            }
            return View(products);
        }
        //GET -- UPSERT
        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> CategoryDropDown = _db.Categories.Select(x => new SelectListItem
            //{
            //    Text = x.Name,
            //    Value = x.Id.ToString()
            //});

            //ViewBag.CategoryDropDown = CategoryDropDown;    
            //Product product = new Product();
            ProductViewModel productViewModel = new ProductViewModel()
            {
                Product = new Product(),
                CategorySelectList = _db.Categories.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
             };
            if (id == null)
            {
                return View(productViewModel);
            } 
            else
            {
                productViewModel.Product = _db.Products.Find(id);
                if (productViewModel.Product == null)
                {
                    return NotFound();
                }
                return View(productViewModel);
            }
        }

        //POST -- CREATE
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product product)
        {
            if (ModelState.IsValid)
            {
                _db.Products.Add(product);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //GET -- DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Products.Find(id);
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
            var obj = _db.Products.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Products.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

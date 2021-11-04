using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVCDotNet5.Data;
using MVCDotNet5.Models;
using MVCDotNet5.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MVCDotNet5.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> products = _db.Products.Include(x => x.Category);
            return View(products);
        }
        //GET -- UPSERT
        public IActionResult Upsert(int? id)
        {
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

        // POST - UPSERT
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;

                if (productViewModel.Product.Id == 0)
                {
                    //Creating
                    string upload = webRootPath + WC.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productViewModel.Product.Image = fileName + extension;
                    _db.Products.Add(productViewModel.Product);
                }
                else
                {
                    //Updating  

                    var productFromDb = _db.Products.AsNoTracking().FirstOrDefault(x => x.Id == productViewModel.Product.Id);

                    if (files.Count > 0)
                    {
                        string upload = webRootPath + WC.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);

                        //Editing or Removing Old Image
                        var oldProductImage = Path.Combine(upload, productFromDb.Image);

                        if (System.IO.File.Exists(oldProductImage))
                        {
                            System.IO.File.Delete(oldProductImage);
                        }

                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        productViewModel.Product.Image = fileName + extension;
                    }
                    productViewModel.Product.Image = productFromDb.Image;
                    _db.Products.Add(productViewModel.Product);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            productViewModel.CategorySelectList = _db.Categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(productViewModel);

        }

        //GET -- DELETE
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }        
            Product product = _db.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        //POST -- DELETE
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;
            var productFromDb = _db.Products.FirstOrDefault(x => x.Id == id);
            if (id == null)
            {
                return NotFound();
            }

            if (files.Count > 0)
            {
                string upload = webRootPath + WC.ImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extension = Path.GetExtension(files[0].FileName);

                //Editing or Removing Old Image
                var oldProductImage = Path.Combine(upload, productFromDb.Image);

                if (System.IO.File.Exists(oldProductImage))
                {
                    System.IO.File.Delete(oldProductImage);
                };
            }
            _db.Products.Remove(productFromDb);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

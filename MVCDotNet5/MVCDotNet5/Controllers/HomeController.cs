﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MVCDotNet5.Data;
using MVCDotNet5.Models;
using MVCDotNet5.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MVCDotNet5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;  
        }

        public IActionResult Index()
        {
            HomeViewModel home = new HomeViewModel()
            {
                Products = _db.Products.Include(x => x.Category).Include(x => x.ApplicationType),
                Categories = _db.Categories
            };
            return View(home);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            DetailsViewModal detailsViewModal = new DetailsViewModal()
            {
                Product = _db.Products.Include(x => x.Category).Include(x => x.ApplicationType)
                .Where(x => x.Id == id).FirstOrDefault(),
                ExistsInCart = false
            };
            return View(detailsViewModal);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

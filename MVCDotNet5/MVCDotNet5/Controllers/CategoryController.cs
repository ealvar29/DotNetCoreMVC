using Microsoft.AspNetCore.Mvc;

namespace MVCDotNet5.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

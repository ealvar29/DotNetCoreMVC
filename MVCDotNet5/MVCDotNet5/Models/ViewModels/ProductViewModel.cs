using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MVCDotNet5.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> Products { get; set; }

        public IEnumerable<Category> Categories { get; set; }

    }
}

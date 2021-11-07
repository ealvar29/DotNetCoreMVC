using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace MVCDotNet5.Models.ViewModels
{
    public class ProductViewModel
    {
        public Product Product { get; set; }

        public IEnumerable<SelectListItem> CategorySelectList {  get; set; }

        public IEnumerable<SelectListItem> ApplicationTypeSelectList { get; set; }
    }
}

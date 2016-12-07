using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogExampleStatic.Web.Data;
using Microsoft.AspNetCore.Mvc;

namespace BlogExampleStatic.Web.Controllers
{
    public class HomeController: Controller
    {
        private readonly BlogExampleDbContext dbContext;

        public HomeController(BlogExampleDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult About()
        {
            return View();
        }
    }
}

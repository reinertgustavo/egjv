using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SmartGarcom.Models;

namespace SmartGarcom.Areas.Admin.Controllers
{  
    public class HomeController : BaseAdminController
    {
        public HomeController(Banco db, IHostingEnvironment env) : base(db, env) { }
        public IActionResult Index()
        {
            return View();
        }
    }
}
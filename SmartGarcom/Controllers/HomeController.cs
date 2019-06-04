using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartGarcom.Models;


namespace SmartGarcom.Controllers
{
    public class HomeController : Controller
    {
        private Banco db;

        public HomeController(Banco _db)
        {
            this.db = _db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult PageNotFound()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        [HttpGet]
        public JsonResult GetCompanies(string query)
        {

            List<object> result = new List<object>();

            List<Company> companies = db.Companies.Where(m => m.Name.ToLower().Contains(query.ToLower())).ToList();

            foreach(var company in companies)
            {
                result.Add(new
                {
                    companyId = company.CompanyId,
                    companyName = company.Name
                });
            }

            return Json(result);
        }

        public ActionResult Search(string cName)
        {
            var names = db.Companies.Where(p => p.Name.Contains(cName)).Select(p => p.Name).ToList();
            return Json(names);
        }
    }
}

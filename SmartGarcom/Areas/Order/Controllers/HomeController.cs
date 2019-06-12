using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartGarcom.Models;
using SmartGarcom.ViewModels;

namespace SmartGarcom.Areas.Order.Controllers
{

    public class HomeController : BaseOrderController
    {
        public HomeController(Banco _db) : base(_db)
        {
        }

        [HttpGet]
        public IActionResult Index(string cName)
        {
            Company company = db.Companies.Where(m => m.Name.ToLower().Equals(cName.ToLower()) ).FirstOrDefault();

            HomeCompanyVM vm = new HomeCompanyVM
            {
                CompanyName = company.Name,
                Assets = db.Assets.Where(m => m.Company.CompanyId == company.CompanyId).ToList(),
                Companies = db.Companies.Find(company.CompanyId),
                CompanyImagePath = company.ImagePath
            };
            return View(vm);
        }
    }
}

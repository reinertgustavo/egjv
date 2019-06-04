using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartGarcom.Models;
using SmartGarcom.ViewModels;

namespace SmartGarcom.Areas.Admin.Controllers
{
   
    public class CompaniesController : BaseAdminController
    {
        public CompaniesController(Banco db, IHostingEnvironment env) : base(db, env) { }

        public IActionResult Index()
        {
            var currentUser = (TUser)ViewBag.TUser;
         
                var companies = db.Companies.ToList();
                return View(companies);   
        }

        [HttpGet]
        public IActionResult Create()
        {
            CompanyVM vm = new CompanyVM();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(CompanyVM vm)
        {
            if (ModelState.IsValid)
            {
                Company company = new Company
                {
                    Name = vm.Name,
                    Cnpj = vm.Cnpj,
                    SocialName = vm.SocialName,
                    CommercialNumber = vm.CommercialNumber,
                    ZipCode = vm.ZipCode,
                    State = vm.State,
                    City = vm.City,
                    Neighborhood = vm.Neighborhood,
                    StreetAddress = vm.StreetAddress,
                    StreetNumber = vm.StreetNumber,
                };
                company.ImagePath = UploadImage(vm.Image, "logos", company.Name);
                db.Companies.Add(company);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else
            {
                return View(vm);
            }
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            Company company = this.db.Companies
                                     .Where(x => x.CompanyId == Id)
                                     .FirstOrDefault();
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }
        [HttpPost]
        public IActionResult Edit(long Id, Company company)
        {
            if (ModelState.IsValid)
            {
                Company companyDb = this.db.Companies.Find(Id);
                companyDb.Name = company.Name;
                companyDb.Cnpj = company.Cnpj;
                companyDb.Name = company.Name;
                companyDb.SocialName = company.SocialName;
                companyDb.CommercialNumber = company.CommercialNumber;
                companyDb.ZipCode = company.ZipCode;
                companyDb.State = company.State;
                companyDb.City = company.City;
                companyDb.Neighborhood = company.Neighborhood;
                companyDb.StreetAddress = company.StreetAddress;
                companyDb.StreetNumber = company.StreetNumber;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(company);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            Company company = this.db.Companies
                              .Where(x => x.CompanyId == id)
                              .FirstOrDefault();

            if (company == null)
            {
                return NotFound();
            }

            return View(company);
        }

        [HttpPost]
        public IActionResult Delete(long id, Company company)
        {
            Company companyDb = this.db.Companies
                                    .Where(x => x.CompanyId == id)
                                    .FirstOrDefault();
            if (ViewBag.TUser.Role.RoleId == 1 && companyDb.IsDeleted == true)
            {
                db.Companies.Remove(companyDb);
                var tables = db.Tables.Where(m => m.Company.CompanyId == companyDb.CompanyId).IgnoreQueryFilters().ToList();
                foreach (var table in tables)
                {
                    db.Tables.Remove(table);
                }
                var categories = db.ProductCategories.Where(m => m.Company.CompanyId == companyDb.CompanyId).IgnoreQueryFilters().ToList();
                foreach(var category in categories)
                {
                    db.ProductCategories.Remove(category);
                }
                var products = db.Products.Where(m => m.Company.CompanyId == companyDb.CompanyId).IgnoreQueryFilters().ToList();
                foreach (var product in products)
                {
                    db.Products.Remove(product);
                }
                var users = db.TUsers.Where(m => m.Company.CompanyId == companyDb.CompanyId).IgnoreQueryFilters().ToList();
                foreach (var user in users)
                {
                    db.TUsers.Remove(user);
                }

            }
            else
            {
                companyDb.IsDeleted = true;
            }
            db.Companies.Remove(companyDb);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}
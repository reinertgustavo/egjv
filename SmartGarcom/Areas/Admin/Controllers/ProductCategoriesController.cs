using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartGarcom.Models;
using SmartGarcom.ViewModels;

namespace SmartGarcom.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductCategoriesController : BaseAdminController
    {
        public ProductCategoriesController(Banco db, IHostingEnvironment env) : base(db, env) { }

        public IActionResult Index(int? SelectedCompany)
        {
            var currentUser = (TUser)ViewBag.TUser;
            if (currentUser.Role.RoleId == 1)
            {
                var categories = db.ProductCategories.Include(x => x.Company).IgnoreQueryFilters().ToList();
                return View(categories);
            }
            else 
            {
                var categories = db.ProductCategories
                .Where(x => x.Company.CompanyId == currentUser.Company.CompanyId)
                .ToList();
                return View(categories);

            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductCategoryVM vm = new ProductCategoryVM
            {
                Companies = ListaComapany()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(ProductCategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                ProductCategory category = new ProductCategory
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    
            };
                if (ViewBag.TUser.Role.RoleId != 1)
                {
                    category.Company = db.Companies.Find(ViewBag.TUser.Company.CompanyId);
                }
                else
                {
                    category.Company = db.Companies.Find(vm.SelectedCompanyId);
                }
                category.ImagePath = UploadImage(vm.Image, "categories", category.Company.Name);
                this.db.ProductCategories.Add(category);
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            ProductCategory category = db.ProductCategories
                                     .Where(x => x.ProductCategoryId == Id)
                                     .IgnoreQueryFilters()
                                     .FirstOrDefault();

            if (category == null)
            {
                return NotFound();
            }
            ProductCategoryVM vm = new ProductCategoryVM
            {
                ProductCategoryId = category.ProductCategoryId,
                Name = category.Name,
                Description = category.Description,
            };


            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(long Id, ProductCategoryVM vm)
        {
            if (ModelState.IsValid)
            {
                ProductCategory category = this.db.ProductCategories.Find(Id);
                category.Name = vm.Name;
                category.Description = vm.Description;
                category.ImagePath = UploadImage(vm.Image,  "categories" ,category.Company.Name);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            ProductCategory category = db.ProductCategories.IgnoreQueryFilters().Where(m => m.ProductCategoryId == id).FirstOrDefault();


            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        public IActionResult Delete(long id, ProductCategoryVM vm) {
            ProductCategory category = db.ProductCategories.IgnoreQueryFilters().Where(m => m.ProductCategoryId == id).FirstOrDefault();

            if (ViewBag.TUser.Role.RoleId == 1 && category.IsDeleted == true)
            {
                List<Product> products = db.Products.Include(m => m.ProductCategory).Where(m => m.ProductCategory.ProductCategoryId == id).ToList();
                if (products != null)
                {
                    foreach( Product product in products)
                    {
                        db.Products.Remove(product);
                    }
                }
                db.ProductCategories.Remove(category);
            }
            else
            {
                category.IsDeleted = true;
            }
            
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        



    }
}














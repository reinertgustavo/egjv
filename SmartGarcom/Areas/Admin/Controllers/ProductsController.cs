using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartGarcom.Models;
using SmartGarcom.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace SmartGarcom.Areas.Admin.Controllers
{
    public class ProductsController : BaseAdminController
    {
        public ProductsController(Banco db, IHostingEnvironment env) : base(db, env) { }

        public List<SelectListItem> ListaCategories()
        {
            var currentUser = (TUser)ViewBag.TUser;
            ProductVM vm = new ProductVM();
            if (currentUser.Role.RoleId == 1)
            {
                var categories = db.ProductCategories.IgnoreQueryFilters().ToList();
                foreach (var category in categories)
                {
                    vm.ProductCategory.Add(new SelectListItem
                    {
                        Value = category.ProductCategoryId.ToString(),
                        Text = category.Name + " - " + category.Company.Name + " - " + category.IsDeleted
                    });
                }
            }
            else
            {
                var categories = db.ProductCategories
                                   .Include( m=> m.Company)
                                   .Where(m => m.Company.CompanyId == currentUser.Company.CompanyId)
                                   .ToList();
                foreach (var category in categories)
                {
                    vm.ProductCategory.Add(new SelectListItem
                    {
                        Value = category.ProductCategoryId.ToString() ,
                        Text = category.Name
                    });
                }
            }
            return vm.ProductCategory;

        }

        public IActionResult Index(int? SelectedCompany, int?SelectedCategory)
        {
            var currentUser = (TUser)ViewBag.TUser;
            if (currentUser.Role.RoleId == 1)
            {
                var products = db.Products.Include(x => x.Company).Include(_ => _.ProductCategory).IgnoreQueryFilters().ToList();
                return View(products);
            }
            else 
            {
                var products = db.Products
                .Include(_ => _.ProductCategory)
                .Where(x => x.Company.CompanyId == currentUser.Company.CompanyId)
                .ToList();
                foreach(var product in products)
                {
                    if(product.ProductCategory == null)
                    {
                        Product product_tmp = db.Products
                                                .Include(_ => _.ProductCategory)
                                                .IgnoreQueryFilters()
                                                .Where(m => m.ProductId.Equals(product.ProductId))
                                                .FirstOrDefault();
                        product.ProductCategory = product_tmp.ProductCategory;
                    }                    
                }

                return View(products);
                
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            ProductVM vm = new ProductVM
            {
                Companies = ListaComapany(),
                ProductCategory = ListaCategories()
            };
            
            return View(vm);
        }
        
        [HttpPost]
        public IActionResult Create(ProductVM vm)
        {
            if(ModelState.IsValid){
                Product product = new Product
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    Price = vm.Price,
                    ProductCategory = db.ProductCategories.Find(vm.SelectedProductCategoryId)  
                };
                if (ViewBag.TUser.Role.RoleId != 1)
                {
                    product.Company = db.Companies.Find(ViewBag.TUser.Company.CompanyId);
                }
                else
                {
                    product.Company = db.Companies.Find(vm.SelectedCompanyId);
                }
                product.ImagePath = UploadImage(vm.Image, "products");
                this.db.Products.Add(product);
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }
            

            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            Product product = this.db.Products
                                     .Include(m => m.ProductCategory)
                                     .Where(x => x.ProductId == Id)
                                     .IgnoreQueryFilters()
                                     .FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }
            ProductVM vm = new ProductVM
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                Companies = ListaComapany(),
                SelectedCompanyId = product.Company.CompanyId,
                ProductCategory = ListaCategories()
            };
            if(product.ProductCategory.IsDeleted == false)
            {
                vm.SelectedProductCategoryId = product.ProductCategory.ProductCategoryId;
            }
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(long Id, ProductVM vm)
        {
            if (ModelState.IsValid)
            {
                Product product = this.db.Products.Find(Id);
                product.Name = vm.Name;
                product.Description = vm.Description;
                product.Price = vm.Price;
                product.Company = db.Companies.Find(vm.SelectedCompanyId);
                product.ProductCategory = db.ProductCategories.Find(vm.SelectedProductCategoryId);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Delete(long id, ProductVM vM)
        {
            Product product = this.db.Products.IgnoreQueryFilters().Where(m => m.ProductId == id).FirstOrDefault();

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            Product product = db.Products.IgnoreQueryFilters().Where(m => m.ProductId == id).FirstOrDefault();
            if (ViewBag.TUser.Role.RoleId == 1 && product.IsDeleted == true)
            {                
                db.Products.Remove(product);
            }
            else
            {
                product.IsDeleted = true;
            }
            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
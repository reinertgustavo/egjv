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
    public class AssetsController : BaseAdminController
    {
        public AssetsController(Banco db, IHostingEnvironment env) : base(db, env) { }

        public List<SelectListItem> ListaCategories()
        {
            var currentUser = (TUser)ViewBag.TUser;
            AssetVM vm = new AssetVM();
            if (currentUser.Role.RoleId == 1)
            {
                var types = db.AssetTypes.IgnoreQueryFilters().ToList();
                foreach (var type in types)
                {
                    vm.AssetType.Add(new SelectListItem
                    {
                        Value = type.AssetTypeId.ToString(),
                        Text = type.Name + " - " + type.Company.Name + " - " + type.IsDeleted
                    });
                }
            }
            else
            {
                var types = db.AssetTypes
                                   .Include( m=> m.Company)
                                   .Where(m => m.Company.CompanyId == currentUser.Company.CompanyId)
                                   .ToList();
                foreach (var type in types)
                {
                    vm.AssetType.Add(new SelectListItem
                    {
                        Value = type.AssetTypeId.ToString() ,
                        Text = type.Name
                    });
                }
            }
            return vm.AssetType;

        }

        public IActionResult Index(int? SelectedCompany, int?SelectedCategory)
        {
            var currentUser = (TUser)ViewBag.TUser;
            if (currentUser.Role.RoleId == 1)
            {
                var Assets = db.Assets.Include(x => x.Company).Include(_ => _.AssetType).IgnoreQueryFilters().ToList();
                return View(Assets);
            }
            else 
            {
                var Assets = db.Assets
                .Include(_ => _.AssetType)
                .Where(x => x.Company.CompanyId == currentUser.Company.CompanyId)
                .ToList();
                foreach(var asset in Assets)
                {
                    if(asset.AssetType == null)
                    {
                        Asset product_tmp = db.Assets
                                                .Include(_ => _.AssetType)
                                                .IgnoreQueryFilters()
                                                .Where(m => m.AssetId.Equals(asset.AssetId))
                                                .FirstOrDefault();
                        asset.AssetType = product_tmp.AssetType;
                    }                    
                }

                return View(Assets);
                
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            AssetVM vm = new AssetVM
            {
                Companies = ListaComapany(),
                AssetType = ListaCategories()
            };
            
            return View(vm);
        }
        
        [HttpPost]
        public IActionResult Create(AssetVM vm)
        {
            if(ModelState.IsValid){
                Asset asset = new Asset
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    Price = vm.Price,
                    AssetType = db.AssetTypes.Find(vm.SelectedProductCategoryId)  
                };
                if (ViewBag.TUser.Role.RoleId != 1)
                {
                    asset.Company = db.Companies.Find(ViewBag.TUser.Company.CompanyId);
                }
                else
                {
                    asset.Company = db.Companies.Find(vm.SelectedCompanyId);
                }
                asset.ImagePath = UploadImage(vm.Image, "Assets");
                this.db.Assets.Add(asset);
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }
            

            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            Asset asset = this.db.Assets
                                     .Include(m => m.AssetType)
                                     .Where(x => x.AssetId == Id)
                                     .IgnoreQueryFilters()
                                     .FirstOrDefault();

            if (asset == null)
            {
                return NotFound();
            }
            AssetVM vm = new AssetVM
            {
                AssetId = asset.AssetId,
                Name = asset.Name,
                Price = asset.Price,
                Description = asset.Description,
                Companies = ListaComapany(),
                SelectedCompanyId = asset.Company.CompanyId,
                AssetType = ListaCategories()
            };
            if(asset.AssetType.IsDeleted == false)
            {
                vm.SelectedProductCategoryId = asset.AssetType.AssetTypeId;
            }
            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(long Id, AssetVM vm)
        {
            if (ModelState.IsValid)
            {
                Asset asset = this.db.Assets.Find(Id);
                asset.Name = vm.Name;
                asset.Description = vm.Description;
                asset.Price = vm.Price;
                asset.Company = db.Companies.Find(vm.SelectedCompanyId);
                asset.AssetType = db.AssetTypes.Find(vm.SelectedProductCategoryId);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Delete(long id, AssetVM vM)
        {
            Asset asset = this.db.Assets.IgnoreQueryFilters().Where(m => m.AssetId == id).FirstOrDefault();

            if (asset == null)
            {
                return NotFound();
            }

            return View(asset);
        }

        [HttpPost]
        public IActionResult Delete(long id)
        {
            Asset asset = db.Assets.IgnoreQueryFilters().Where(m => m.AssetId == id).FirstOrDefault();
            if (ViewBag.TUser.Role.RoleId == 1 && asset.IsDeleted == true)
            {                
                db.Assets.Remove(asset);
            }
            else
            {
                asset.IsDeleted = true;
            }
            db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
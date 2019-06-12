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
    public class AssetTypesController : BaseAdminController
    {
        public AssetTypesController(Banco db, IHostingEnvironment env) : base(db, env) { }

        public IActionResult Index(int? SelectedCompany)
        {
            var currentUser = (TUser)ViewBag.TUser;
            if (currentUser.Role.RoleId == 1)
            {
                var types = db.AssetTypes.Include(x => x.Company).IgnoreQueryFilters().ToList();
                return View(types);
            }
            else 
            {
                var types = db.AssetTypes
                .Where(x => x.Company.CompanyId == currentUser.Company.CompanyId)
                .ToList();
                return View(types);

            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            AssetTypeVM vm = new AssetTypeVM
            {
                Companies = ListaComapany()
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(AssetTypeVM vm)
        {
            if (ModelState.IsValid)
            {
                AssetType type = new AssetType
                {
                    Name = vm.Name,
                    Description = vm.Description,
                    
            };
                if (ViewBag.TUser.Role.RoleId != 1)
                {
                    type.Company = db.Companies.Find(ViewBag.TUser.Company.CompanyId);
                }
                else
                {
                    type.Company = db.Companies.Find(vm.SelectedCompanyId);
                }
                type.ImagePath = UploadImage(vm.Image, "types", type.Company.Name);
                this.db.AssetTypes.Add(type);
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            AssetType type = db.AssetTypes
                                     .Where(x => x.AssetTypeId == Id)
                                     .IgnoreQueryFilters()
                                     .FirstOrDefault();

            if (type == null)
            {
                return NotFound();
            }
            AssetTypeVM vm = new AssetTypeVM
            {
                AssetTypeId = type.AssetTypeId,
                Name = type.Name,
                Description = type.Description,
            };


            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(long Id, AssetTypeVM vm)
        {
            if (ModelState.IsValid)
            {
                AssetType type = this.db.AssetTypes.Find(Id);
                type.Name = vm.Name;
                type.Description = vm.Description;
                type.ImagePath = UploadImage(vm.Image,  "types" ,type.Company.Name);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            AssetType type = db.AssetTypes.IgnoreQueryFilters().Where(m => m.AssetTypeId == id).FirstOrDefault();


            if (type == null)
            {
                return NotFound();
            }

            return View(type);
        }

        [HttpPost]
        public IActionResult Delete(long id, AssetTypeVM vm) {
            AssetType type = db.AssetTypes.IgnoreQueryFilters().Where(m => m.AssetTypeId == id).FirstOrDefault();

            if (ViewBag.TUser.Role.RoleId == 1 && type.IsDeleted == true)
            {
                List<Asset> Assets = db.Assets.Include(m => m.AssetType).Where(m => m.AssetType.AssetTypeId == id).ToList();
                if (Assets != null)
                {
                    foreach( Asset asset in Assets)
                    {
                        db.Assets.Remove(asset);
                    }
                }
                db.AssetTypes.Remove(type);
            }
            else
            {
                type.IsDeleted = true;
            }
            
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        



    }
}














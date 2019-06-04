using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartGarcom.Models;
using SmartGarcom.ViewModels;

namespace SmartGarcom.Areas.Admin.Controllers
{
    public class TablesController : BaseAdminController
    {

        public TablesController(Banco db, IHostingEnvironment env) : base(db, env) { }

        public IActionResult Index(int? SelectedCompany)
        {
            var currentUser = (TUser)ViewBag.TUser;
            if (currentUser.Role.RoleId == 1)
            {
                var tables = db.Tables.ToList();
                return View(tables);
            }
            else if (currentUser.Role.RoleId == 2)
            {
                var tables = db.Tables
                .Include(x => x.Company)
                .Where(x => x.Company.CompanyId == currentUser.Company.CompanyId)
                .ToList();
                return View(tables);
            }
            return View();
        }
    

        [HttpGet]
        public IActionResult Create()
        {
            TableVM vm = new TableVM
            {
                Companies = ListaComapany()
            };
            return View(vm);
        }
    
        [HttpPost]
        public IActionResult Create(TableVM vm)
        {
            
            if (ModelState.IsValid)
            {
                var currentUser = (TUser)ViewBag.TUser;
                Table table = new Table
                {
                    Number = vm.Number,
                    
                    QRCode = "https://api.qrserver.com/v1/create-qr-code/?size=150x150&data=https://app.helko.com.br/Order/OrderCards/Home/Create/?nMesa=" + vm.Number + "&comp=" + vm.SelectedCompanyId,
                    
                };
                if (currentUser.Role.RoleId == 1) 
                {
                    table.Company = db.Companies.Find(vm.SelectedCompanyId);
                }
                else
                {
                    table.Company = db.Companies.Find(currentUser.Company.CompanyId);
                }
                this.db.Tables.Add(table);
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }
        [HttpGet]
        public IActionResult Edit(long Id)
        {
            Table table = this.db.Tables
                                     .Where(x => x.TableId == Id)
                                     .FirstOrDefault();

            if (table == null)
            {
                return NotFound();
            }
            TableVM vm = new TableVM
            {
                TableId = table.TableId,
                Number = table.Number,
                Companies = ListaComapany(),
                SelectedCompanyId = table.Company.CompanyId
            };


            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(long Id, TableVM vm)
        {
            if (ModelState.IsValid)
            {
                Table table = this.db.Tables.Find(Id);
                table.Number = vm.Number;
                table.Company = db.Companies.Find(vm.SelectedCompanyId);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            Table table = this.db.Tables
                              .Where(x => x.TableId == id)
                              .FirstOrDefault();

            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        [HttpPost]
        public IActionResult Delete(long id, Table product)
        {
            Table table = this.db.Tables
                                .Where(x => x.TableId == id)
                                .FirstOrDefault();
            if (ViewBag.TUser.Role.RoleId == 1 && table.IsDeleted == true)
            {
                db.Tables.Remove(table);
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

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
    public class StatusController : BaseAdminController
    {

        public StatusController(Banco db, IHostingEnvironment env) : base(db, env) { }

        public IActionResult Index(int? SelectedCompany)
        {
            var currentUser = (TUser)ViewBag.TUser;
            var status = db.Status.ToList();
            return View(status);
        }


        [HttpGet]
        public IActionResult Create()
        {
            StatusVM vm = new StatusVM();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(StatusVM vm)
        {

            if (ModelState.IsValid)
            {
                var currentUser = (TUser)ViewBag.TUser;
                Status status = new Status();
                this.db.Status.Add(status);
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }
        [HttpGet]
        public IActionResult Edit(long Id)
        {
            Status status = this.db.Status
                                     .Where(x => x.StatusID == Id)
                                     .FirstOrDefault();

            if (status == null)
            {
                return NotFound();
            }
            StatusVM vm = new StatusVM
            {
                StatusID = status.StatusID,
                Name = status.Name,
                Description = status.Description
            };


            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(long Id, StatusVM vm)
        {
            if (ModelState.IsValid)
            {
                Status status = this.db.Status.Find(Id);
                status.Name = vm.Name;
                status.Description = vm.Description;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            Status status = this.db.Status
                              .Where(x => x.StatusID == id)
                              .FirstOrDefault();

            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        [HttpPost]
        public IActionResult Delete(long id, Status status)
        {
            Status statusDb = this.db.Status
                                .Where(x => x.StatusID == id)
                                .FirstOrDefault();
            db.Status.Remove(statusDb);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}

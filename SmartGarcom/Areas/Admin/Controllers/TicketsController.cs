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
    [Area("Admin")]
    public class TicketsController : BaseAdminController
    {

        public TicketsController(Banco db, IHostingEnvironment env) : base(db, env) { }

        public IActionResult Index(int? SelectedCompany, int? SelectedAsset)
        {
            var currentUser = (TUser)ViewBag.TUser;
            if (currentUser.Role.RoleId == 1)
            {
                var tickets = db.Tickets.Include(x => x.Company).Include(x => x.Asset).IgnoreQueryFilters().ToList(); 
                return View(tickets);
            }
            else if (currentUser.Role.RoleId == 2)
            {
                var tickets = db.Tickets
                .Include(x => x.Company).Where(x => x.Company.CompanyId == currentUser.Company.CompanyId).Include(x => x.Asset).ToList();
                return View(tickets);
            }
            return View();
        }
    

        [HttpGet]
        public IActionResult Create()
        {
            TicketVM vm = new TicketVM
            {
                Companies = ListaComapany(),
                Assets = ListaAsset()
            };
            return View(vm);
        }
    
        [HttpPost]
        public IActionResult Create(TicketVM vm)
        {
            
            if (ModelState.IsValid)
            {
                var currentUser = (TUser)ViewBag.TUser;
                Ticket ticket = new Ticket
                {
                    Name = vm.Name,
                    Assunto = vm.Assunto,
                    Descricao = vm.Descricao,
                    Status = vm.Status,
                    Responsavel = vm.Responsavel,
                    PrevisaoConclusao = vm.PrevisaoConclusao,
                    Asset = db.Assets.Find(vm.SelectedAssetId)
                };
                if (currentUser.Role.RoleId == 1) 
                {
                    ticket.Company = db.Companies.Find(vm.SelectedCompanyId);
                }
                else
                {
                    ticket.Company = db.Companies.Find(currentUser.Company.CompanyId);
                }
                ticket.DataAbertura = DateTime.Now;
                this.db.Tickets.Add(ticket);
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }
        [HttpGet]
        public IActionResult Edit(long Id)
        {
            Ticket ticket = this.db.Tickets
                                     .Where(x => x.TicketId == Id)
                                     .FirstOrDefault();

            if (ticket == null)
            {
                return NotFound();
            }
            TicketVM vm = new TicketVM
            {
                TicketId = ticket.TicketId,
                Name = ticket.Name,
                Companies = ListaComapany(),
                SelectedCompanyId = ticket.Company.CompanyId
            };


            return View(vm);
        }
        [HttpPost]
        public IActionResult Edit(long Id, TicketVM vm)
        {
            if (ModelState.IsValid)
            {
                Ticket ticket = this.db.Tickets.Find(Id);
                ticket.Name = vm.Name;
                ticket.Company = db.Companies.Find(vm.SelectedCompanyId);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            Ticket ticket = this.db.Tickets
                              .Where(x => x.TicketId == id)
                              .FirstOrDefault();

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        [HttpPost]
        public IActionResult Delete(long id, Ticket asset)
        {
            Ticket ticket = this.db.Tickets
                                .Where(x => x.TicketId == id)
                                .FirstOrDefault();
            if (ViewBag.TUser.Role.RoleId == 1 && ticket.IsDeleted == true)
            {
                db.Tickets.Remove(ticket);
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

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
                Assets = ListaAsset(),
                TUsers = ListaTUser()
            };
            return View(vm);
        }
    
        [HttpPost]
        public IActionResult Create(TicketVM vm)
        {
            
            if (ModelState.IsValid)
            {
                Ticket ticket = new Ticket
                {
                    Name = vm.Name,
                    Assunto = vm.Assunto,
                    Descricao = vm.Descricao,
                    Status = vm.Status,
                    EmailSolicitante = vm.EmailSolicitante,
                    PrevisaoConclusao = vm.PrevisaoConclusao,
                    Asset = db.Assets.Find(vm.SelectedAssetId),
                    TUser = db.TUsers.Find(vm.SelectedUserId)
                };
                if (ViewBag.TUser.Role.RoleId == 1) 
                {
                    ticket.Company = db.Companies.Find(vm.SelectedCompanyId);
                }
                else
                {
                    ticket.Company = db.Companies.Find(ViewBag.TUser.Company.CompanyId);
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
            var currentUser = (TUser)ViewBag.TUser;
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
                Companies = ListaComapany(),
                Assets = ListaAsset(),
                TUsers = ListaTUser(),
                SelectedAssetId = ticket.Asset.AssetId,
                SelectedUserId = ticket.TUser.TUserId,
                SelectedCompanyId = ticket.Company.CompanyId,
                Assunto = ticket.Assunto,
                Descricao = ticket.Descricao,
                Status = ticket.Status,
                EmailSolicitante = ticket.EmailSolicitante,
                PrevisaoConclusao = ticket.PrevisaoConclusao
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
                ticket.Assunto = vm.Assunto;
                ticket.Descricao = vm.Descricao;
                ticket.Status = vm.Status;
                ticket.EmailSolicitante = vm.EmailSolicitante;
                ticket.PrevisaoConclusao = vm.PrevisaoConclusao;
                ticket.Asset = db.Assets.Find(vm.SelectedAssetId);
                ticket.TUser = db.TUsers.Find(vm.SelectedUserId);
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
        public IActionResult Delete(long id, Ticket ticket)
        {
            Ticket ticketDb = this.db.Tickets
                                .Where(x => x.TicketId == id)
                                .FirstOrDefault();
            db.Tickets.Remove(ticketDb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

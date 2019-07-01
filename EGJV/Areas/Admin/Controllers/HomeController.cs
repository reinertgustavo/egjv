using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EGJV.Models;
using EGJV.ViewModels;

namespace EGJV.Areas.Admin.Controllers
{  
    public class HomeController : BaseAdminController
    {
        public HomeController(Banco db, IHostingEnvironment env) : base(db, env) { }
        public IActionResult Index()
        {
            HomeVM vm = new HomeVM
            {
                Companies = ListaComapany(),
                Assets = ListaAsset(),
                TUsers = ListaTUser(),
                Tickets = ListaTicket(),
                TicketsAbertos = ListaTicketAberto(),
                TicketsAtrasados = ListaTicketAtrasado(),
                TicketsFechados = ListaTicketFechado(),
            };
            ViewBag.TicketsAbertos = vm.TicketsAbertos.Count;
            ViewBag.TicketsAtrasados = vm.TicketsAtrasados.Count;
            ViewBag.TicketsFechados = vm.TicketsFechados.Count;
            return View();
        }
    }
}
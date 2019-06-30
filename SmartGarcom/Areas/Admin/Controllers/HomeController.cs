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
            };
            ViewBag.TicketsAbertos = vm.TicketsAbertos.Count;
            return View();
        }
    }
}
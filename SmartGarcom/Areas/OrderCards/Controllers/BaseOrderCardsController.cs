using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SmartGarcom.Models;
using SmartGarcom.ViewModels;
using SmartGarcom.Filters;

namespace SmartGarcom.Areas.OrderCards.Controllers
{
    [ServiceFilter(typeof(AuthOrderCard))]
    [Area("OrderCards")]
    public class BaseOrderCardsController : Controller
    {
        protected Banco db;

        public BaseOrderCardsController(Banco _db)
        {
            this.db = _db;
        }
        public List<SelectListItem> ListaComapany()
        {
            OrderCardVM vm = new OrderCardVM();
            var companies = db.Companies.ToList();
            foreach (var company in companies)
            {
                vm.Companies.Add(new SelectListItem
                {
                    Value = company.CompanyId.ToString(),
                    Text = company.Name
                });
            }
            return vm.Companies;

        }

        public List<SelectListItem> ListaTickets()
        {
            var currentUser = (TUser)ViewBag.Tuser;
            OrderCardVM vm = new OrderCardVM();

            if(currentUser.Role.RoleId == 1)
            {
                var tickets = db.Tickets
                               .Include(t=> t.Company)
                               .ToList();
                foreach (var ticket in tickets)
                {
                    vm.Tickets.Add(new SelectListItem
                    {
                        Value = ticket.TicketId.ToString(),
                        Text = ticket.Name + "-" + ticket.Company.Name
                    });
                }
            }
            else
            {
                var tickets = db.Tickets
                               .Include(t => t.Company)
                               .Where(t => t.Company.CompanyId == currentUser.Company.CompanyId)
                               .ToList();
                foreach (var ticket in tickets)
                {
                    vm.Tickets.Add(new SelectListItem
                    {
                        Value = ticket.TicketId.ToString(),
                        Text = ticket.Name
                    });
                }
            }
            
            return vm.Tickets;

        }
    }
}
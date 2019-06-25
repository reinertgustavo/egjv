using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartGarcom.Models;
using SmartGarcom.ViewModels;

namespace SmartGarcom.Areas.OrderCards.Controllers
{
    public class HomeController : BaseOrderCardsController
    {
        public HomeController(Banco _db) : base(_db)
        {
        }

        public IActionResult Index(long type = 0)
        {
            if (ViewBag.OrderCard != null)
            {
                var currentUser = (TUser)ViewBag.TUser;
                var currentOrder = (OrderCard)ViewBag.OrderCard;
                if (type == 0)
                {
                    var types = db.AssetTypes
                                           .Where(m => m.Company.CompanyId == currentOrder.Company.CompanyId)
                                           .ToList();
                    OrderCardVM vm = new OrderCardVM
                    {
                        AssetTypes = types

                    };
                    var Assets = db.Assets
                                           .Include(m => m.AssetType)
                                           .Where(m => m.Company.CompanyId == currentOrder.Company.CompanyId)
                                           .ToList();
                    vm.Assets = Assets;
                    return View(vm);
                }
                else
                {
                    return View("Index");
                }
            }
            else return View("Create");
            
            


            
        }

        [HttpGet]
        public IActionResult Create(string nMesa, long comp)
        {
            
            Ticket table = db.Tickets
                            .Include(t => t.Company)
                            .Where(t => t.Name == nMesa && t.Company.CompanyId == comp)
                            .FirstOrDefault();
            OrderCardVM vm = new OrderCardVM
            {
                SelectedTicketId = table.TicketId,
                SelectedCompanyId = table.Company.CompanyId,
            };

            var currentUser = (TUser)ViewBag.TUser;
            var orderToken = Request.Cookies[OrderCard.COOKIE_ORDERCARD_TOKEN_NAME];
            if(orderToken != null)
            {
                OrderCard orderCard = db.OrderCards
                            .Include(t => t.Company)
                            .Include(t => t.Ticket)
                            .Where(t => t.orderCardToken == orderToken)
                            .FirstOrDefault();
                if (orderCard != null && orderCard.orderCardToken == orderToken)
                {
                    RedirectToAction("Index");
                }    
            }
            if(currentUser != null && currentUser.Role.RoleId != 5)
            {
                if (currentUser.Role.RoleId == 1)
                {
                    vm.Companies = ListaComapany();
                }
                vm.Tickets = ListaTickets();
                return View(vm);
            }
           else
            {
                return View(vm);
            }
        }
        [HttpPost]
        public IActionResult Create(OrderCardVM vm)
        {
            if (ModelState.IsValid){
                OrderCard orderCard = new OrderCard
                {
                    Company = db.Companies.Find(vm.SelectedCompanyId),
                    Ticket = db.Tickets.Find(vm.SelectedTicketId),
                    orderCardToken = OrderCard.GenerateOrderCardToken()
                };
                TUser user = db.TUsers.Where(m => m.CPF == vm.CPF).FirstOrDefault();
                if (user == null)
                {
                    user.Name = vm.Name;
                    user.CPF = vm.CPF;
                    user.Email = vm.Email;
                    user.Company = db.Companies.Find(1);
                    user.Role = db.Roles.Find(5);
                    this.db.TUsers.Add(user);
                    this.db.SaveChanges();
                    orderCard.User = db.TUsers.Where(m => m.CPF == vm.CPF).FirstOrDefault();
                }
                else
                {
                    orderCard.User = user;

                }
                this.db.OrderCards.Add(orderCard);
                this.db.SaveChanges();
                Response.Cookies.Append(OrderCard.COOKIE_ORDERCARD_TOKEN_NAME, orderCard.orderCardToken);
                return RedirectToAction("Index");

            }
            return View(vm);
        }
    }
}
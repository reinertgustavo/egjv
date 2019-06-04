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

        public IActionResult Index(long category = 0)
        {
            if (ViewBag.OrderCard != null)
            {
                var currentUser = (TUser)ViewBag.TUser;
                var currentOrder = (OrderCard)ViewBag.OrderCard;
                if (category == 0)
                {
                    var categories = db.ProductCategories
                                           .Where(m => m.Company.CompanyId == currentOrder.Company.CompanyId)
                                           .ToList();
                    OrderCardVM vm = new OrderCardVM
                    {
                        ProductCategories = categories

                    };
                    var products = db.Products
                                           .Include(m => m.ProductCategory)
                                           .Where(m => m.Company.CompanyId == currentOrder.Company.CompanyId)
                                           .ToList();
                    vm.Products = products;
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
            
            Table table = db.Tables
                            .Include(t => t.Company)
                            .Where(t => t.Number == nMesa && t.Company.CompanyId == comp)
                            .FirstOrDefault();
            OrderCardVM vm = new OrderCardVM
            {
                SelectedTableId = table.TableId,
                SelectedCompanyId = table.Company.CompanyId,
            };

            var currentUser = (TUser)ViewBag.TUser;
            var orderToken = Request.Cookies[OrderCard.COOKIE_ORDERCARD_TOKEN_NAME];
            if(orderToken != null)
            {
                OrderCard orderCard = db.OrderCards
                            .Include(t => t.Company)
                            .Include(t => t.Table)
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
                vm.Tables = ListaTables();
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
                    Table = db.Tables.Find(vm.SelectedTableId),
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
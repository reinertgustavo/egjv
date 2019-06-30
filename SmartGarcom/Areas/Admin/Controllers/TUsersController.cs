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
    public class TUsersController : BaseAdminController
    {
        public TUsersController(Banco db, IHostingEnvironment env) : base(db, env) { }

        public List<SelectListItem> ListaRoles()
        {
            var currentUser = (TUser)ViewBag.TUser;
            UserVM vm = new UserVM();
            if (currentUser.Role.RoleId == 1)
            {
                var roles = db.Roles.ToList();
                foreach (var role in roles)
                {
                    vm.Roles.Add(new SelectListItem
                    {
                        Value = role.RoleId.ToString(),
                        Text = role.Name
                    });
                }
            }
            else
            {
                var roles = db.Roles
                             .Where(m => m.RoleId != 1)
                             .ToList();
                foreach (var role in roles)
                {
                    vm.Roles.Add(new SelectListItem
                    {
                        Value = role.RoleId.ToString(),
                        Text = role.Name
                    });
                }

            }
            return vm.Roles;

        }

        public IActionResult Index(int? SelectedCompany, int? SelectedRole)
        {
            var currentUser = (TUser)ViewBag.TUser;
            if (currentUser.Role.RoleId == 1)
            {
                var companies = db.Companies.OrderBy(q => q.Name).ToList();
                ViewBag.SelectedCompany = new SelectList(companies, "Id", "Name", SelectedCompany);
                int companyID = SelectedCompany.GetValueOrDefault();

                var roles = db.Roles.OrderBy(q => q.Name.ToList());
                ViewBag.SelectedRole = new SelectList(roles, "Id", "Name", SelectedRole);
                int roleID = SelectedRole.GetValueOrDefault();
                var users = db.TUsers
                              .Include(d => d.Role)
                              .Include(d => d.Company)
                              .OrderBy(d => d.TUserId);
                return View(users.ToList());
            }
            else if (currentUser.Role.RoleId == 2)
            {
                var roles = db.Roles.OrderBy(q => q.Name.ToList());
                ViewBag.SelectedRole = new SelectList(roles, "Id", "Name", SelectedRole);
                int roleID = SelectedRole.GetValueOrDefault();

                var users = db.TUsers
                .Include(x => x.Company)
                .Include(d => d.Role)
                .Where(x => x.Company.CompanyId == currentUser.Company.CompanyId)
                .ToList();
                return View(users);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Create()
        {
            UserVM vm = new UserVM();
            var currentUser = (TUser)ViewBag.TUser;
            vm.Companies = ListaComapany();
            vm.Roles = ListaRoles();

            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(UserVM vm)
        {
            if (ModelState.IsValid)
            {
                TUser usuario = new TUser
                {
                    Name = vm.Name,
                    Email = vm.Email,
                    Birthdate = vm.Birthdate,
                    Password = TUser.GenerateHash(vm.Password),
                    Role = db.Roles.Find(vm.SelectedRoleId)
                };
                if (ViewBag.TUser.Role.RoleId != 1)
                {
                    usuario.Company = db.Companies.Find(ViewBag.TUser.Company.CompanyId);
                }
                else
                {
                    usuario.Company = db.Companies.Find(vm.SelectedCompanyId);
                }
                this.db.TUsers.Add(usuario);
                this.db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Edit(long Id)
        {
            TUser usuario = this.db.TUsers 
                                     .Include(m => m.Company)
                                     .Where(x => x.TUserId == Id)
                                     .FirstOrDefault();
            var currentUser = (TUser)ViewBag.TUser;
            if (usuario == null)
            {
                return NotFound();
            }

            UserVM vm = new UserVM
            {
                TUserId = usuario.TUserId,
                Name = usuario.Name,
                Email = usuario.Email,
                Birthdate = usuario.Birthdate,
                Companies = ListaComapany(),
                SelectedCompanyId = usuario.Company.CompanyId,
                Roles = ListaRoles(),
                SelectedRoleId = usuario.Role.RoleId
            };

            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(long Id, UserVM vm)
        {
            if (ModelState.IsValid)
            {
                TUser usuarioDb = this.db.TUsers.Find(Id);
                usuarioDb.Name = vm.Name;
                usuarioDb.Email = vm.Email;
                usuarioDb.Birthdate = vm.Birthdate;
                if(vm.Password != null)
                {
                    usuarioDb.Password = TUser.GenerateHash(vm.Password);
                }
                if (ViewBag.TUser.Role.RoleId != 1)
                {
                    ticket.Company = db.Companies.Find(ViewBag.TUser.Company.CompanyId);
                }
                else
                {
                    ticket.Company = db.Companies.Find(vm.SelectedCompanyId);
                }
                usuarioDb.Role = db.Roles.Find(vm.SelectedRoleId);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vm);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            TUser usuario = this.db.TUsers
                                .Include(m => m.Company)
                                .Where(x => x.TUserId == id)
                                .FirstOrDefault();
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Delete(long id, TUser usuario)
        {
            TUser usuarioDb = this.db.TUsers
                                  .Include(m => m.Company)
                                  .Where(x => x.TUserId == id)
                                  .FirstOrDefault();

            db.TUsers.Remove(usuarioDb);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
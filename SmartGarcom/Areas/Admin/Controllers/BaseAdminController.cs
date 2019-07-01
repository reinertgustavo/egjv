using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartGarcom.Filters;
using SmartGarcom.Models;
using SmartGarcom.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace SmartGarcom.Areas.Admin.Controllers
{
    [ServiceFilter(typeof(AuthAdmin))]
    [Area("Admin")]
    public class BaseAdminController : Controller
    {
        protected Banco db;
        protected IHostingEnvironment env;

        public BaseAdminController(Banco _db, IHostingEnvironment _env)
        {
            this.db = _db;
            this.env = _env;

        }
        public List<SelectListItem> ListaComapany()
        {
            UserVM vm = new UserVM();
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
        public List<SelectListItem> ListaAsset()
        {
            UserVM vm = new UserVM();
            var assets = db.Assets.ToList();
            foreach (var asset in assets)
            {
                vm.Assets.Add(new SelectListItem
                {
                    Value = asset.AssetId.ToString(),
                    Text = asset.Name
                });
            }
            return vm.Assets;
        }

        public List<SelectListItem> ListaTUser()
        {
            UserVM vm = new UserVM();
            var currentUser = (TUser)ViewBag.TUser;
            var tusers = db.TUsers.Include(x => x.Company).Where(x => x.Company.CompanyId == currentUser.Company.CompanyId).ToList();
            foreach (var tuser in tusers)
            {
                vm.TUsers.Add(new SelectListItem
                {
                    Value = tuser.TUserId.ToString(),
                    Text = tuser.Name
                });
            }
            return vm.TUsers;
        }
        public List<SelectListItem> ListaTicket()
        {
            TicketVM vm = new TicketVM();
            var currentUser = (TUser)ViewBag.TUser;
            var tickets = db.Tickets.Include(x => x.Company).Where(x => x.Company.CompanyId == currentUser.Company.CompanyId).ToList();
            foreach (var ticket in tickets)
            {
                vm.Tickets.Add(new SelectListItem
                {
                    Value = ticket.TicketId.ToString(),
                    Text = ticket.Name
                });
            }
            return vm.Tickets;
        }
        public List<SelectListItem> ListaTicketAberto()
        {
            TicketVM vm = new TicketVM();
            var currentUser = (TUser)ViewBag.TUser;
            var tickets = db.Tickets.Include(x => x.Company).Where(x => x.Company.CompanyId == currentUser.Company.CompanyId).Where(d => d.Status == "Aberto" || d.Status == "Novo" || d.Status == "Em progresso").ToList();
            foreach (var ticket in tickets)
            {
                vm.Tickets.Add(new SelectListItem
                {
                    Value = ticket.TicketId.ToString(),
                    Text = ticket.Name
                });
            }
            return vm.Tickets;
        }
        public List<SelectListItem> ListaTicketAtrasado()
        {
            TicketVM vm = new TicketVM();
            var currentUser = (TUser)ViewBag.TUser;
            var tickets = db.Tickets.Include(x => x.Company).Where(x => x.Company.CompanyId == currentUser.Company.CompanyId).Where(d => d.PrevisaoConclusao < DateTime.Now).ToList();
            foreach (var ticket in tickets)
            {
                vm.Tickets.Add(new SelectListItem
                {
                    Value = ticket.TicketId.ToString(),
                    Text = ticket.Name
                });
            }
            return vm.Tickets;
        }

        public String UploadImage(IFormFile formFile, string ctl_path , String CompanyName = null)
        {
            if (formFile != null && formFile.Length != 0)
            {
                String company = CompanyName.ToLower().Normalize();
                String extension = Path.GetExtension(formFile.FileName);
                String fileName = $"{Guid.NewGuid().ToString()}{extension}";
                string path = Path.Combine(env.WebRootPath, "images","companies", company, ctl_path , fileName).ToLower();

                Directory.CreateDirectory(Path.GetDirectoryName(path));
                using (FileStream fileStream = new FileStream(path, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }

                return $"/images/companies/{company}/{ctl_path}/{fileName}".ToLower();
            }

            return null;
        }

    }
}
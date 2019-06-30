using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.ViewModels
{
    public class HomeVM
    {
        public HomeVM()
        {
            this.Companies = new List<SelectListItem>();
            this.Assets = new List<SelectListItem>();
            this.Tickets = new List<SelectListItem>();
            this.TicketsAbertos = new List<SelectListItem>();
        }

        [Display(Name = "Empresa")]
        public List<SelectListItem> Companies { get; set; }
        public List<SelectListItem> Tickets { get; set; }
        public List<SelectListItem> TicketsAbertos { get; set; }
        public long SelectedTicketId { get; set; }
        public long SelectedCompanyId { get; set; }

        [Display(Name = "Ativo")]
        public List<SelectListItem> Assets { get; set; }
        public long SelectedAssetId { get; set; }

        [Display(Name = "Responsável")]
        public List<SelectListItem> TUsers { get; set; }
        public long SelectedUserId { get; set; }

    }
}

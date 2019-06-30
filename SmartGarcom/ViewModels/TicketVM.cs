using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.ViewModels
{
    public class TicketVM
    {
        public TicketVM()
        {
            this.Companies = new List<SelectListItem>();
            this.Assets = new List<SelectListItem>();
            this.Tickets = new List<SelectListItem>();
        }

        [Display(Name = "Id do Ticket")]
        public long TicketId { get; set; }

        [Display(Name = "Codigo Interno")]
        public long InternalTicketId { get; set; }

        [Display(Name = "Empresa")]
        public List<SelectListItem> Companies { get; set; }
        public List<SelectListItem> Tickets { get; set; }
        public long SelectedCompanyId { get; set; }

        [Display(Name = "Ativo")]
        public List<SelectListItem> Assets { get; set; }
        public long SelectedAssetId { get; set; }

        [Display(Name = "Nome do Ticket")]
        public String Name { get; set; }

        [Display(Name = "Assunto do Ticket")]
        public String Assunto { get; set; }

        [Display(Name = "Status")]
        public String Status { get; set; }

        [Display(Name = "Responsável")]
        public List<SelectListItem> TUsers { get; set; }
        public long SelectedUserId { get; set; }

        [Display(Name = "E-mail do Solicitante")]
        public String EmailSolicitante { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Abertura do Ticket")]
        public DateTime DataAbertura { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Informe a previsão de conclusão do Ticket")]
        [Display(Name = "Previsão de conclusão do ticket")]
        public DateTime PrevisaoConclusao { get; set; }

        [Display(Name = "Descricao")]
        public String Descricao { get; set; }
    }
}

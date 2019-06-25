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
        }

        [Display(Name = "Id do Ticket")]
        public long TicketId { get; set; }

        [Display(Name = "Codigo Interno")]
        public long InternalTicketId { get; set; }

        [Display(Name = "Empresa")]
        public List<SelectListItem> Companies { get; set; }
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
        public String Responsavel { get; set; }

        [Display(Name = "Data de abertura")]
        public String DataAbertura { get; set; }

        [Display(Name = "Previsão de Conclusão")]
        public String PrevisaoConclusao { get; set; }

        [Display(Name = "Descricao")]
        public String Descricao { get; set; }
    }
}

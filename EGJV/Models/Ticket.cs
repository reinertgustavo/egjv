using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using EGJV.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGJV.Models
{
    public class Ticket
    {
        public long TicketId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Asset Asset { get; set; }		
        public string Name { get; set; }
        public string Assunto { get; set; }	
        public string Descricao { get; set; }
        public string Status { get; set; }        
        public string EmailSolicitante { get; set; }        
        public virtual TUser TUser { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data de Abertura do Ticket")]
        public DateTime DataAbertura { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Informe a previsão de conclusão do Ticket")]
        [Display(Name = "Previsão de conclusão do ticket")]
        public DateTime PrevisaoConclusao { get; set; }
        public bool IsDeleted { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.ViewModels
{
    public class TableVM
    {
        public TableVM()
        {
            this.Companies = new List<SelectListItem>();
        }

        [Display(Name = "Codigo")]
        public long TableId { get; set; }

        [Display(Name = "Codigo Interno")]
        public long InternalTableId { get; set; }

        [Display(Name = "Empresa")]
        public List<SelectListItem> Companies { get; set; }
        public long SelectedCompanyId { get; set; }

        [Display(Name = "Numero da Mesa")]
        public String Number { get; set; }

        [Display(Name = "QR-Code")]
        public String QRCode { get; set; }
    }
}

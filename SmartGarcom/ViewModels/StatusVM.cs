using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.ViewModels
{
    public class StatusVM
    {
        [Display(Name = "Codigo do Status")]
        public long StatusID { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o nome do Status")]
        public String Name { get; set; }

        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Informe a descrição do Status")]
        public String Description { get; set; }
    }
}

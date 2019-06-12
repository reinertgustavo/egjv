using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SmartGarcom.ViewModels
{
    public class AssetTypeVM
    {
        public AssetTypeVM()
        {
            this.Companies = new List<SelectListItem>();
        }

        [Display(Name = "Código")]
        public long AssetTypeId { get; set; }

        public List<SelectListItem> Companies { get; set; }
        [Display(Name = "Empresa")]
        public long SelectedCompanyId { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Display(Name = "Imagem")]
        public IFormFile Image { get; set; }
    }
}

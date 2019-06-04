using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace SmartGarcom.ViewModels
{
    public class ProductCategoryVM
    {
        public ProductCategoryVM()
        {
            this.Companies = new List<SelectListItem>();
        }

        [Display(Name = "Codigo")]
        public long ProductCategoryId { get; set; }

        public List<SelectListItem> Companies { get; set; }
        [Display(Name = "Empresa")]
        public long SelectedCompanyId { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Descriçao")]
        public string Description { get; set; }

        [Display(Name = "Imagem")]
        public IFormFile Image { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGJV.ViewModels
{
    public class AssetVM
    {
        public AssetVM()
        {
            this.Companies = new List<SelectListItem>();
            this.AssetType = new List<SelectListItem>();
        }

        [Display(Name = "Codigo")]
        public long AssetId { get; set; }

        [Display(Name = "Empresa")]
        public long SelectedCompanyId { get; set; }
        public List<SelectListItem> Companies { get; set; }

        [Display(Name = "Categorias")]
        public long SelectedProductCategoryId { get; set; }
        public List<SelectListItem> AssetType { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Informe o Preço")]
        [Display(Name = "Preço")]
        public double Price { get; set; }

        [Display(Name = "Imagem")]
        public IFormFile Image { get; set; }

        [Display(Name = "Status")]
        public string IsActive { get; set; }
    }
}

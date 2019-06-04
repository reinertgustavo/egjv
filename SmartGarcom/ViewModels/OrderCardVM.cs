using Microsoft.AspNetCore.Mvc.Rendering;
using SmartGarcom.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.ViewModels
{
    public class OrderCardVM
    {
        public OrderCardVM()
        {
            this.Companies = new List<SelectListItem>();
            //ProductCategories = new List<SelectListItem>();
            this.Tables = new List<SelectListItem>();
        }

        [Display(Name = "Codigo")]
        public long ProductId { get; set; }

        public long SelectedTableId { get; set; }
        public List<SelectListItem> Tables { get; set; }

        [Display(Name = "Empresa")]
        public long SelectedCompanyId { get; set; }
        public List<SelectListItem> Companies { get; set; }
        public Company Company { get; set; }

        [Display(Name = "Categorias")]
        public long SelectedProductCategoryId { get; set; }
        public List<ProductCategory> ProductCategories { get; set; }
        public ProductCategory ProductCategory { get; set; }

        public long SelectedProductId { get; set; }
        public List<Product> Products { get; set; }
        public Product Product { get; set; }

        [Required(ErrorMessage = "Informe o Nome")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Informe o CPF")]
        [Display(Name = "CPF")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "Informe o Email")]
        [Display(Name = "Email")]
        public string Email { get; set; }


    }
}

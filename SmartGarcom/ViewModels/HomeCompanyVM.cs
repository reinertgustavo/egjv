using Microsoft.AspNetCore.Mvc.Rendering;
using SmartGarcom.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.ViewModels
{
    public class HomeCompanyVM
    {
        public HomeCompanyVM()
        {
        }

        public Company Companies { get; set; }
        public string CompanyName { get; set; }

        public string CompanyImagePath { get; set; }

        [Display(Name = "Empresa")]
        public List<Product> Products { get; set; }
        public long SelectedProductId { get; set; }

        public ProductCategory Categories { get; set; }
        public string CategoryName { get; set; }
       
        
    }
}

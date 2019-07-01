using Microsoft.AspNetCore.Mvc.Rendering;
using EGJV.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGJV.ViewModels
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
        public List<Asset> Assets { get; set; }
        public long SelectedProductId { get; set; }

        public AssetType Categories { get; set; }
        public string CategoryName { get; set; }
       
        
    }
}

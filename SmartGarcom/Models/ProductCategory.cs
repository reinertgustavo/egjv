using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.Models
{
    public class ProductCategory
    {

        public long ProductCategoryId { get; set; }

        [Display(Name = "Empresa")]
        public long CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public String ImagePath { get; set; }

        public List<Product> Products { get; set; }

        public bool IsDeleted { get; set; }


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EGJV.Models
{
    public class AssetType
    {

        public long AssetTypeId { get; set; }

        [Display(Name = "Empresa")]
        public long CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public String ImagePath { get; set; }

        public List<Asset> Assets { get; set; }

        public bool IsDeleted { get; set; }


    }
}

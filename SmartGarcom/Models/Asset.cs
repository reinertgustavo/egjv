using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.Models
{
    public class Asset
    {
        public long AssetId { get; set; }

        [Display(Name = "Empresa")]
        public long CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        [Display(Name = "Categorias")]
        public long AssetTypeId { get; set; }
        [ForeignKey("AssetTypeId")]
        public virtual AssetType AssetType { get; set; }

        [Display(Name="Nome")]
        public string Name { get; set; }

        [Display(Name="Descrição")]
        public string Description { get; set; }

        [Display(Name="Preço")]
        public double Price { get; set; }

        [Display(Name="Imagem")]
        public string ImagePath { get; set; }

        [Display(Name = "Status")]
        public bool IsDeleted { get; set; }


      
    }





}


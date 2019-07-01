using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EGJV.Models
{
    public class Company
    {
        public long CompanyId { get; set; }

        [Required(ErrorMessage = "Informe o CNPJ da empresa")]
        [Display(Name = "CNPJ")]
        public String Cnpj { get; set; }

        [Required(ErrorMessage = "Informe o nome da empresa")]
        [Display(Name = "Nome da Empresa")]
        public String Name { get; set; }

        [Display(Name = "Razão Social")]
        public String SocialName { get; set; }

        [Display(Name = "Numero Comercial")]
        public String CommercialNumber { get; set; }

        [Required(ErrorMessage = "Informe o CEP da empresa")]
        [Display(Name = "Codigo Postal(CEP)")]
        public String ZipCode { get; set; }

        [Display(Name = "Estado")]
        public String State { get; set; }

        [Display(Name = "Cidade")]
        public String City { get; set; }

        [Display(Name = "Bairro")]
        public String Neighborhood { get; set; }

        [Display(Name = "Endereço")]
        public String StreetAddress { get; set; }

        [Display(Name = "Numero")]
        public String StreetNumber { get; set; }

        [Display(Name = "Imagem")]
        public string ImagePath { get; set; }

        public List<Asset> Assets { get; set; }
        public List<AssetType> AssetTypes { get; set; }
        public List<Ticket> Tickets { get; set; }
        public List<TUser> Users { get; set; }

        public bool IsDeleted { get; set; }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartGarcom.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.ViewModels
{
    public class UserVM
    {
        public UserVM()
        {
            this.Companies = new List<SelectListItem>();
            this.Assets = new List<SelectListItem>();
            this.Roles = new List<SelectListItem>();
            this.TUsers = new List<SelectListItem>();
        }

        [Display(Name = "Codigo do Usuario")]
        public long TUserId { get; set; }

        
        [Display(Name = "Senha")]
        public String Password { get; set; }

        [Required(ErrorMessage = "Informe o e-mail")]
        [Display(Name = "E-mail")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Informe o nome")]
        [Display(Name = "Nome")]
        public String Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Informe a data de nascimento")]
        [Display(Name = "Data de Nascimento")]
        public DateTime Birthdate { get; set; }

        public long PermissionId { get; set; }

        public List<SelectListItem> Companies { get; set; }
        public List<SelectListItem> Assets { get; set; }
        public List<SelectListItem> TUsers { get; set; }

        public List<SelectListItem> Roles { get; set; }

        [Display(Name = "Permissão")]
        [Required(ErrorMessage = "Informe a permissão do usuário")]
        public long SelectedRoleId { get; set; }

        [Display(Name = "Ativo")]
        [Required(ErrorMessage = "Selecione o Ativo")]
        public long SelectedAssetId { get; set; }

        [Display(Name = "Usuário")]
        [Required(ErrorMessage = "Selecione o usuário")]
        public long SelectedTUserId { get; set; }

        [Display(Name = "Empresa")]
        [Required(ErrorMessage = "Informe a empresa")]
        public long SelectedCompanyId { get; set; }
    }
}

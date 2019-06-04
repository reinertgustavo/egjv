using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.ViewModels
{
    public class SignUpVM
    {
        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Informe o seu nome")]
        public String Name { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Informe o seu email")]
        public String Email { get; set; }

        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Informe a sua senha")]
        public String Password { get; set; }

        [Display(Name = "Confirmação da senha")]
        [Required(ErrorMessage = "Informe a confirmação da senha")]
        public String PasswordConfirm { get; set; }
    }
}

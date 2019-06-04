using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartGarcom.Models
{
    public class TUser
    {
        public static string COOKIE_AUTH_TOKEN_NAME = "auth_token";

        public long TUserId { get; set; }

        public string AuthToken { get; set; }
        
        public String CPF { get; set; }

        [Required(ErrorMessage = "Informe o e-mail")]
        [Display(Name = "E-mail")]
        public String Email { get; set; }

        [Required(ErrorMessage = "Informe o nome" )]
        [Display(Name = "Nome")]
        public String Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "Informe a data de nascimento")]
        [Display(Name = "Data de Nascimento")]
        public DateTime Birthdate { get; set; }

        [Display(Name = "Senha")]
        public String Password { get; set; }

        public virtual Company Company { get; set; }

        [Display(Name ="Permissão")]
        public virtual Role Role { get; set; }
        public bool IsDeleted { get; set; }

        public static String GenerateHash(String password)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.Unicode.GetBytes(password));
            return Encoding.Unicode.GetString(hash);
        }

        public static String GenerateAuthToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

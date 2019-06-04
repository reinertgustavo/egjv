using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SmartGarcom.Models;

namespace SmartGarcom.ViewModels
{
    public class CommonVM
    {
        public LoginVM Loginvm { get; set; }
        public SignUpVM SignUpvm { get; set; }
    }

}

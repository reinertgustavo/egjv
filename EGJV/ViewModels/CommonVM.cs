using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EGJV.Models;

namespace EGJV.ViewModels
{
    public class CommonVM
    {
        public LoginVM Loginvm { get; set; }
        public SignUpVM SignUpvm { get; set; }
    }

}

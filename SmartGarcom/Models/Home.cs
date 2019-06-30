using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartGarcom.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.Models
{
    public class Home
    {
        public long TicketId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Asset Asset { get; set; }		
        public virtual Ticket Ticket { get; set; }   
        public virtual TUser TUser { get; set; }
    }
}

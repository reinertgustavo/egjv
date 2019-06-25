using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.Models
{
    public class Ticket
    {
        public long TicketId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Asset Asset { get; set; }		
        public string Name { get; set; }
        public string Assunto { get; set; }	
        public string Descricao { get; set; }
        public string Status { get; set; }        
        public string Responsavel { get; set; }
        public bool IsDeleted { get; set; }
    }
}

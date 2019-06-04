using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.Models
{
    public class Table
    {
        public long TableId { get; set; }

        public virtual Company Company { get; set; }

        public string Number { get; set; }

        public string QRCode { get; set; }
        public bool IsDeleted { get; set; }
    }
}

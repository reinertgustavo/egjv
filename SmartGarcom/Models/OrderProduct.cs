using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.Models
{
    public class OrderProduct
    {
        public long OrderProductId { get; set; }

        public virtual Order Order { get; set; }

        public virtual Asset Asset { get; set; }

        public long Quantity { get; set; }
    }
}

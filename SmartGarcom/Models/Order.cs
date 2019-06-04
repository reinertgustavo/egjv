using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.Models
{
    public class Order
    {
        public long OrderId{ get; set; }

        public virtual OrderCard OrderCard { get; set; }

        public virtual Status Status { get; set; }

    }
}

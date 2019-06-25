using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.Models
{
    public class OrderCard
    {
        public static string COOKIE_ORDERCARD_TOKEN_NAME = "orderc_token";

        public static String GenerateOrderCardToken()
        {
            return Guid.NewGuid().ToString();
        }

        public String orderCardToken { get; set; }

        public long OrderCardId { get; set; }
        
        public virtual Company Company { get; set; }

        public virtual Ticket Ticket { get; set; }

        public virtual TUser User { get; set; }

        


    }
}

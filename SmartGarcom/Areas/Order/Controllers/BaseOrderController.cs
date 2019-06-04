using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SmartGarcom.Models;

namespace SmartGarcom.Areas.Order.Controllers
{
    //[ServiceFilter(typeof())]
    [Area("Order")]
    public class BaseOrderController : Controller
    {
        protected Banco db;

        public BaseOrderController(Banco _db)
        {
            this.db = _db;
        }
    }
}
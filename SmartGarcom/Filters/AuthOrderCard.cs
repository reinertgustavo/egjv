using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using SmartGarcom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.Filters
{
    public class AuthOrderCard : ActionFilterAttribute
    {
        private Banco db;

        public AuthOrderCard(Banco _db)
        {
            this.db = _db;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            var request = context.HttpContext.Request;
            var order_token = request.Cookies[OrderCard.COOKIE_ORDERCARD_TOKEN_NAME];
            var orderCard = db.OrderCards
                         .Include(m => m.Company)
                         .Include(m => m.Ticket)
                         .Include(m => m.User)
                         .Where(m => m.orderCardToken.Equals(order_token) && m.orderCardToken != null)
                         .FirstOrDefault();
            var controller = context.Controller as Controller;
            controller.ViewBag.OrderCard = orderCard;
        }
    }
}

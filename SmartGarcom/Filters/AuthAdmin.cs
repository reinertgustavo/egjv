using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using SmartGarcom.Controllers;
using SmartGarcom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartGarcom.Filters
{
    public class AuthAdmin : ActionFilterAttribute
    {
        private Banco db;

        public AuthAdmin(Banco _db)
        {
            this.db = _db;
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);
        }
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var auth_token = request.Cookies[TUser.COOKIE_AUTH_TOKEN_NAME];
            var user = db.TUsers
                         .Include(m => m.Role)
                         .Where(m => m.AuthToken.Equals(auth_token) && m.AuthToken != null)
                         .FirstOrDefault();

            if(user == null || user.Role.RoleId != 1 && user.Role.RoleId != 2)
            {
                context.Result = new RedirectToRouteResult(
                    new RouteValueDictionary
                    {
                        { "area", "" },
                        { "controller", "Auth" },
                        { "action", "AccessDenied" }

                    });
            }
            else
            {
                var controller = context.Controller as Controller;
                controller.ViewBag.TUser = user;
            }
        }
    }
}

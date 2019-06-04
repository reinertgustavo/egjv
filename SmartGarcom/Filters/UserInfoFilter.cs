using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using SmartGarcom.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartGarcom.Filters
{
    public class UserInfoFilter : IActionFilter
    {
        private Banco db;
        public UserInfoFilter(Banco _db)
        {
            this.db = _db;
        }

        public void OnActionExecuted(ActionExecutedContext context) { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var request = context.HttpContext.Request;
            var auth_token = request.Cookies[TUser.COOKIE_AUTH_TOKEN_NAME];
            var user = db.TUsers
                            .Include(m => m.Company)
                            .Include(m => m.Role)
                            .Where(m => m.AuthToken.Equals(auth_token) && m.AuthToken != null)
                            .FirstOrDefault();
            if (auth_token != null && auth_token != "")
            {
                var controller = context.Controller as Controller;
                controller.ViewBag.TUser = user;
                
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using System;
using WebDev_MainLab.Models;

namespace WebDev_MainLab
{
    public class CartNotEmptyAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var cart = context.HttpContext.Session.Get<Cart>("Cart");
            if (cart == null)
                context.HttpContext.Response.Redirect("ToCart");
            
        }
    }
}

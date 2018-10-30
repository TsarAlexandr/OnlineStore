using Microsoft.AspNetCore.Mvc.Filters;
using System;
using WebDev_MainLab.Models;

namespace WebDev_MainLab.Attributes
{
    public class CartNotEmptyAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.HttpContext.Request.Method == "POST")
            {
                var cart = context.HttpContext.Session.Get<Cart>("Cart");
                if (cart != null)
                {
                    cart.Clear();
                    context.HttpContext.Session.Set("Cart", cart);
                }
            }
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var cart = context.HttpContext.Session.Get<Cart>("Cart");
            if (cart == null)
                context.HttpContext.Response.Redirect("ToCart");
            
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using WebDev_MainLab.Models;

namespace WebDev_MainLab.Attributes
{
    public class OrderExistAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if(context.HttpContext.Request.Method == "POST")
                context.HttpContext.Session.SetNull("Order");
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var order = context.HttpContext.Session.Get<Order>("Order");
            if (order == null)
                context.Result = new RedirectToActionResult("Create", "Orders", null);
                
        }
    }
}

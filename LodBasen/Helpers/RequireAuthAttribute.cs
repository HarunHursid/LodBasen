﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.VisualBasic;
using Xunit.Sdk;

namespace LodBasen.Helpers
{
    public class RequireAuthAttribute : Attribute, IPageFilter
    {
        public string RequiredRole { get; set; } = "";
        public void OnPageHandlerExecuted(PageHandlerExecutedContext context)
        { 

        }

        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            string? role = context.HttpContext.Session.GetString("role");

            if (role == null)
            {
                context.Result = new RedirectResult("/GetSuperSælger");
            }
            else 
            {
                if (RequiredRole.Length > 0 && !RequiredRole.Equals(role)) 
                {
                    context.Result = new RedirectResult("/GetSuperSælger");
                }
            }

        }

        public void OnPageHandlerSelected(PageHandlerSelectedContext context)
        {

        }
    }
}

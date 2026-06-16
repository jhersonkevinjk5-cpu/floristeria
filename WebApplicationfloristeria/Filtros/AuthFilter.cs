using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationfloristeria.Filtros
{
    public class AuthFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext contextoFiltro)
        {
            if (HttpContext.Current.Session["usuario"] == null) {
                contextoFiltro.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                    {
                        { "controller", "Auth"},
                        { "action", "Login"}
                    }
                    ); 
            }

            base.OnActionExecuting(contextoFiltro);
        }
    }
}
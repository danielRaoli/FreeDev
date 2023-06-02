using FreeDev.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace FreeDev.Filters
{
    public class FiltroUsuarioDevLogado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string usuarioSessao = context.HttpContext.Session.GetString("sessaoUsuario");
            if(string.IsNullOrEmpty(usuarioSessao))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
            }
            else
            {
                UsuarioDevModel usuario = JsonConvert.DeserializeObject<UsuarioDevModel>(usuarioSessao);
                if(usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }
                if(!(usuario is UsuarioDevModel))
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "HomeLogado" }, { "action", "Index" } });
                }
            }
            base.OnActionExecuting(context);

        }


    }
}

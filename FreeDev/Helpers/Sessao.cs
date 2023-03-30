using FreeDev.Models.Entities;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace FreeDev.Helpers
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public Sessao(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public UsuarioDevModel BuscarSessaoDev()
        {
            string sessaoUsuario = _contextAccessor.HttpContext.Session.GetString("sessaoUsuario");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            return JsonConvert.DeserializeObject<UsuarioDevModel>(sessaoUsuario);
        }

        public UsuarioModel BuscarSessaoNormal()
        {
            string sessaoUsuario = _contextAccessor.HttpContext.Session.GetString("sessaoUsuario");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;

            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }

        public void CriarSessao(UsuarioModel usuario)
        {
            string usuarioJson;
            if(usuario is UsuarioDevModel)
            {
                usuarioJson = JsonConvert.SerializeObject(usuario as UsuarioDevModel);
                _contextAccessor.HttpContext.Session.SetString("sessaoUsuario", usuarioJson);
            }
            usuarioJson = JsonConvert.SerializeObject(usuario);
            _contextAccessor.HttpContext.Session.SetString("sessaoUsuario",usuarioJson);
        }

        public void DeletarSessao()
        {
            _contextAccessor.HttpContext.Session.Remove("sessaoUsuario");
        }
    }
}

using FreeDev.Helpers;
using FreeDev.Models;
using FreeDev.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace FreeDev.Controllers
{
    public class LoginController : Controller
    {
        private readonly ISessao _sessao;
        private readonly UsuarioService _usuarioService;

        public LoginController(ISessao sessao, UsuarioService usuarioService)
        {
            _sessao = sessao;
            _usuarioService = usuarioService;
        }

        public IActionResult Index()
        {
            if (_sessao.BuscarSessaoNormal() != null) return RedirectToAction("Index", "HomeLogado");

            return View();

        }

        [HttpPost]
        public async Task<IActionResult> Entrar(LoginViewModel loginView)
        {
            if (ModelState.IsValid)
            {
                var usuario = _usuarioService.BuscarPorLogin(loginView.Email);
                if(usuario != null)
                {
                    if (usuario.ValidaSenha(loginView.Senha))
                    {
                        _sessao.CriarSessao(usuario);
                        return RedirectToAction("Index", "HomeLogado");
                    }
                }
            }
            return View(loginView);
        }

        public IActionResult Sair()
        {
            _sessao.DeletarSessao();
            return RedirectToAction(nameof(Index));
        }
    }
}

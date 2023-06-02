using FreeDev.Helpers;
using FreeDev.Models.Entities;
using FreeDev.Models.ViewModels;
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
                TempData["alertError"] = "O email do usuário esta incorreto";
                return RedirectToAction("Index", "Login");
            }
            TempData["alertError"] = "A senha ou email do usuário estão incorretos";
            return RedirectToAction("Index","Login");
        }
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioModel usuario)
        {
            if (ModelState.IsValid)
            {
                await _usuarioService.CadastrarUsuario(usuario);
                return RedirectToAction("Index", "HomeLogado");
            }
            TempData["errorAlert"] = "Algo deu errado ao tentar fazer o cadastro, tenha certeza de preencher todos os dados";
            return View(usuario);
        }


        public IActionResult CreateDev()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateDev(UsuarioDevModel usuarioDev)
        {
            if (ModelState.IsValid)
            {
                await _usuarioService.CadastrarUsuario(usuarioDev);
                _sessao.CriarSessao(usuarioDev);
                return RedirectToAction("Index", "HomeLogado");
            }
            TempData["alertError"] = "Algo deu errado ao tentar fazer o cadastro, tenha certeza de preencher todos os dados";
            return View(usuarioDev);
        }

        public IActionResult Logout()
        {
            _sessao.DeletarSessao();
            return RedirectToAction(nameof(Index));
        }
    }
}

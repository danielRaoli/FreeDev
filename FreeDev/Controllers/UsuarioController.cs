using FreeDev.Helpers;
using FreeDev.Models.Entities;
using FreeDev.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreeDev.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;
        private readonly ISessao _sessao;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
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

        public async Task<IActionResult> Perfil()
        {
            var usuarioSessao = _sessao.BuscarSessaoNormal();
            if (usuarioSessao != null)
            {
                return View(usuarioSessao);
            }
            return NotFound();
        }

    }
}

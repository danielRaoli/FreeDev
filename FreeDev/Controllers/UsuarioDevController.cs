using FreeDev.Helpers;
using FreeDev.Models.Entities;
using FreeDev.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreeDev.Controllers
{
    public class UsuarioDevController : Controller
    {
        private readonly ISessao _sessao;
        private readonly UsuarioService _usuarioService;

        public UsuarioDevController(UsuarioService usuarioService, ISessao sessao)
        {
            _usuarioService = usuarioService;
            _sessao = sessao;
        }

        public IActionResult Create()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UsuarioDevModel usuarioDev)
        {
            if (ModelState.IsValid)
            {
               await _usuarioService.CadastrarUsuario(usuarioDev);
                return RedirectToAction("Index", "HomeLogado");
            }
            TempData["alertError"]="Algo deu errado ao tentar fazer o cadastro, tenha certeza de preencher todos os dados";
            return View(usuarioDev);
        }


        public async Task< IActionResult> Perfil()
        {
            var usuarioSessao = _sessao.BuscarSessaoDev();
            if (usuarioSessao != null)
            {
              return View(usuarioSessao); 
            }
            return NotFound();
        }
    }
}

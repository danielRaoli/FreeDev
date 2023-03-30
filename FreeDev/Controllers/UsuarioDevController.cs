using FreeDev.Models.Entities;
using FreeDev.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreeDev.Controllers
{
    public class UsuarioDevController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioDevController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
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
                return RedirectToAction("Index", "Home");
            }
            TempData["alertError"]="Algo deu errado ao tentar fazer o cadastro, tenha certeza de preencher todos os dados";
            return View(usuarioDev);
        }
    }
}

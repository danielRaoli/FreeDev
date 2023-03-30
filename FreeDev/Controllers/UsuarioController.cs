using FreeDev.Models.Entities;
using FreeDev.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreeDev.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly UsuarioService _usuarioService;

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
                return RedirectToAction("Index", "Home");
            }
            TempData["errorAlert"] = "Algo deu errado ao tentar fazer o cadastro, tenha certeza de preencher todos os dados";
            return View(usuario);
        }

        
    }
}

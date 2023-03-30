using FreeDev.Services;
using Microsoft.AspNetCore.Mvc;

namespace FreeDev.Controllers
{
    public class HomeLogadoController : Controller
    {
        private readonly UsuarioService _usuarioService;

        public HomeLogadoController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioService.BuscarTodos();
            return View(usuarios);
        }
    }
}

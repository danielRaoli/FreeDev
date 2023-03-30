using FreeDev.Models;
using Microsoft.AspNetCore.Mvc;

namespace FreeDev.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Entrar(LoginViewModel longinView)
        {
            return View(longinView);
        }
    }
}

using FreeDev.Filters;
using FreeDev.Helpers;
using FreeDev.Models.Entities;
using FreeDev.Models.ViewModels;
using FreeDev.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace FreeDev.Controllers
{
    [FiltroUsuarioDevLogado]
    public class UsuarioDevController : Controller
    {
        private readonly ISessao _sessao;
        private readonly UsuarioService _usuarioService;
        

        public UsuarioDevController(UsuarioService usuarioService, ISessao sessao)
        {
            _usuarioService = usuarioService;
            _sessao = sessao;
            

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


        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            UsuarioDevModel usuarioDb = _usuarioService.BuscarDevPorId(id.Value);
            if(usuarioDb == null)
            {
                return NotFound();
            }
            var usuario = new UsuarioDevSemSenhaModel { Email = usuarioDb.Email, Nome = usuarioDb.Nome, Id = usuarioDb.Id, Telefone = usuarioDb.Telefone, Descricao = usuarioDb.Descricao };
            var viewModel = new AlterarFoto { UsuarioDev = usuario};
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int? id,AlterarFoto viewModel)
        {
            if (ModelState.IsValid)
            {
                if (id.Value != viewModel.UsuarioDev.Id)
                {
                    throw new Exception("Id miss macth");
                }
                using (var memoryStream = new MemoryStream())
                {
                    await viewModel.Arquivo.CopyToAsync(memoryStream);
                    viewModel.UsuarioDev.Foto = memoryStream.ToArray();
                }
                await _usuarioService.AtualizarDevDados(viewModel.UsuarioDev);
                TempData["alertSuccess"] = "Perfil atualizado com sucesso!, algumas alterações só seram feitas após o cliente relogar";
                return RedirectToAction(nameof(Perfil));


            }
            TempData["alertError"] = "Verifique se todos os dados estão preenchidos corretamente!";
            return View(viewModel.UsuarioDev);
        }

        public IActionResult AlterarSenha()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            try
            {
                var usuarioSessao = _sessao.BuscarSessaoDev();
                alterarSenhaModel.Id = usuarioSessao.Id;
                if (ModelState.IsValid)
                {
                   await _usuarioService.AtualizarSenha(alterarSenhaModel);
                    TempData["alertSuccess"] = "Senha Atualizada com sucesso";
                    return RedirectToAction(nameof(Perfil));
                }
                TempData["alertError"] = "Preencha todos os campos corretamente";
                return View(alterarSenhaModel);
            }
            catch (Exception e)
            {
                TempData["alertError"] = e.Message;
                return View(alterarSenhaModel);
            }

        } 
    }
}

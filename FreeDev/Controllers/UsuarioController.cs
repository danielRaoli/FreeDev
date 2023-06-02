using FreeDev.Filters;
using FreeDev.Helpers;
using FreeDev.Models.Entities;
using FreeDev.Models.ViewModels;
using FreeDev.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;
using System.Reflection.Metadata.Ecma335;

namespace FreeDev.Controllers
{
    [FiltroUsuarioLogado]
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

        public IActionResult Perfil()
        {
           var usuarioSessao = _sessao.BuscarSessaoNormal();
            if(usuarioSessao == null)
            {
                return RedirectToAction("Login", "Index");
            }
            return View(usuarioSessao);
        }



        public IActionResult Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var usuarioDb = _usuarioService.BuscarPorId(id.Value);
            if (usuarioDb == null)
            {
                return NotFound();
            }
            var usuario = new UsuarioSemSenhaModel { Email = usuarioDb.Email, Nome = usuarioDb.Nome, Id = usuarioDb.Id, Telefone = usuarioDb.Telefone };
            return View(usuario);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(int? id, UsuarioSemSenhaModel usuarioSemSenha)
        {
            if (id.Value != usuarioSemSenha.Id)
            {
                throw new Exception("Id miss macth");
            }
            if (!ModelState.IsValid)
            {
                TempData["alertError"] = "Verifique se todos os dados estão preenchidos corretamente!";
                return View(usuarioSemSenha);
            }
            await _usuarioService.AtualizarDados(usuarioSemSenha);
            TempData["alertSuccess"] = "Perfil atualizado com sucesso!";
            return RedirectToAction(nameof(Perfil));
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
                var usuarioSessao = _sessao.BuscarSessaoNormal();
                alterarSenhaModel.Id = usuarioSessao.Id;
                if (ModelState.IsValid)
                {
                    if (alterarSenhaModel.NovaSenha.Equals(alterarSenhaModel.ConfirmarSenha))
                    {
                        await _usuarioService.AtualizarSenha(alterarSenhaModel);
                        TempData["alertSuccess"] = "Senha Atualizada com sucesso";
                        return RedirectToAction(nameof(Perfil));
                    }
                    TempData["alertError"] = "A nova senha e a senha confirmada precisam ser iguais";
                    return View(alterarSenhaModel);
                }
                TempData["alertError"] = "Certifique-se de preencher todos os itens corretamentes";
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

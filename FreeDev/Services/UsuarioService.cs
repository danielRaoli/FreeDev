using FreeDev.Data;
using FreeDev.Models.Entities;
using FreeDev.Models.ViewModels;
using Microsoft.AspNetCore.Server.IIS.Core;
using Microsoft.EntityFrameworkCore;

namespace FreeDev.Services
{
    public class UsuarioService
    {
        private readonly FreeDevContext _context;

        public UsuarioService(FreeDevContext freeDevContext)
        {
            _context = freeDevContext;
        }
    
        public UsuarioModel BuscarPorId(int id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Id == id);
        }

        public UsuarioDevModel BuscarDevPorId(int id)
        {
            return _context.UsuariosDev.FirstOrDefault(x => x.Id == id);
        }


        public UsuarioModel BuscarPorLogin(string email)
        {
          return _context.Usuarios.FirstOrDefault(x => x.Email == email);

        }

        public async Task<List<UsuarioDevModel>> BuscarTodos()
        {
            return await _context.UsuariosDev.ToListAsync();
        }

        public async Task AtualizarDados(UsuarioSemSenhaModel usuarioSemSenha)
        {
            bool hasAny = await _context.Usuarios.AnyAsync(x => x.Id == usuarioSemSenha.Id);
            if (!hasAny)
            {
                throw new Exception("Id Not Found");
            }
            var usuarioDb = BuscarPorId(usuarioSemSenha.Id);
   
            usuarioDb.Nome = usuarioSemSenha.Nome;
            usuarioDb.Email = usuarioSemSenha.Email;
            usuarioDb.Telefone = usuarioSemSenha.Telefone;
            usuarioDb.DataAtualizacao = DateTime.Now;
            _context.Usuarios.Update(usuarioDb);
           await  _context.SaveChangesAsync();
        }
        public async Task AtualizarDevDados(UsuarioDevSemSenhaModel usuarioSemSenha)
        {
            bool hasAny = await _context.UsuariosDev.AnyAsync(x => x.Id == usuarioSemSenha.Id);
            if (!hasAny)
            {
                throw new Exception("Id Not Found");
            }
            var usuarioDb =  _context.UsuariosDev.FirstOrDefault(x => x.Id == usuarioSemSenha.Id);
            usuarioDb.Nome = usuarioSemSenha.Nome;
            usuarioDb.FotoPerfil = usuarioSemSenha.Foto;
            usuarioDb.Email = usuarioSemSenha.Email;
            usuarioDb.Telefone = usuarioSemSenha.Telefone;
            usuarioDb.Descricao = usuarioSemSenha.Descricao;
            usuarioDb.DataAtualizacao = DateTime.Now;
            _context.UsuariosDev.Update(usuarioDb);
            await _context.SaveChangesAsync();
        }


        public async Task CadastrarUsuario(UsuarioModel usuario)
        {
            if(usuario is UsuarioDevModel)
            {
               
                usuario.SetSenha();
                usuario.DataCriacao = DateTime.Now;
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
            }
           
            usuario.SetSenha();
            usuario.DataCriacao = DateTime.Now;
            _context.Usuarios.Add(usuario);
           await _context.SaveChangesAsync();   
        }

        public async Task AtualizarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            var usuarioDb = await _context.Usuarios.FirstOrDefaultAsync(x => x.Id == alterarSenhaModel.Id);
            if(usuarioDb == null)
            {
                throw new Exception("Id not found");
            }
            if (!usuarioDb.ValidaSenha(alterarSenhaModel.SenhaAtual)) throw new Exception("Senha atual incorreta, digite novamente");
            if (usuarioDb.ValidaSenha(alterarSenhaModel.NovaSenha)) throw new Exception("A senha atual não pode ser igual a passada");
            if(!alterarSenhaModel.NovaSenha.Equals(alterarSenhaModel.ConfirmarSenha)) throw new Exception("A nova senha e a confirmação não conferem");
            usuarioDb.SetNovaSenha(alterarSenhaModel.NovaSenha); 
            usuarioDb.DataAtualizacao = DateTime.Now;
            _context.Usuarios.Update(usuarioDb);
            await _context.SaveChangesAsync();
        }
    }
}

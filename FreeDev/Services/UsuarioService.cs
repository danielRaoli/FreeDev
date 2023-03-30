using FreeDev.Data;
using FreeDev.Models.Entities;
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


        public UsuarioModel BuscarPorLogin(string email)
        {
          return _context.Usuarios.FirstOrDefault(x => x.Email == email);

        }

        public async Task<List<UsuarioDevModel>> BuscarTodos()
        {
            return await _context.UsuariosDev.ToListAsync();
        }




        public async Task CadastrarUsuario(UsuarioModel usuario)
        {
            usuario.SetSenha();
            usuario.DataCriacao = DateTime.Now;
            _context.Usuarios.Add(usuario);
           await _context.SaveChangesAsync();   
        }
    }
}

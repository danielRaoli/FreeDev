using FreeDev.Data;
using FreeDev.Models.Entities;

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
        public UsuarioModel BuscarPorIdDev(int id)
        {
            return _context.UsuariosDev.FirstOrDefault(x => x.Id == id);
        }



        public async Task CadastrarUsuario(UsuarioModel usuario)
        {
            if(usuario is UsuarioDevModel)
            {
                usuario.DataCriacao = DateTime.Now;
                _context.UsuariosDev.Add((UsuarioDevModel)usuario);
                await _context.SaveChangesAsync();
            }
            usuario.DataCriacao = DateTime.Now;
            _context.Usuarios.Add(usuario);
           await _context.SaveChangesAsync();   
        }
    }
}

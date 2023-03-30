using FreeDev.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FreeDev.Data
{
    public class FreeDevContext : DbContext
    {

        public FreeDevContext(DbContextOptions<FreeDevContext> options) : base(options)
        {

        }

        public DbSet<UsuarioModel> Usuarios {get; set;}
        public DbSet<UsuarioDevModel> UsuariosDev {get; set;}

        

    }
}

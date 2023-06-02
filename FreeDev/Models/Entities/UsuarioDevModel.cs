
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FreeDev.Models.Entities
{
    public class UsuarioDevModel : UsuarioModel
    {

        public byte[]? FotoPerfil { get; set; }

        public string Descricao { get; set; }
        public UsuarioDevModel()
        {

        }

        public UsuarioDevModel(string nome,string email,string telefone,string senha, string descricao) : base( nome, email, telefone,senha) 
        {
            Descricao = descricao;
        }



        
    }
}

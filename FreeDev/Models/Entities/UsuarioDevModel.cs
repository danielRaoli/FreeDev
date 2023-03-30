using System.ComponentModel.DataAnnotations;

namespace FreeDev.Models.Entities
{
    public class UsuarioDevModel : UsuarioModel
    {

        [Display(Name= " Descrição")]
        public string Descricao { get; set; }
        public UsuarioDevModel()
        {

        }

        public UsuarioDevModel(string nome,string email,string telefone,string senha, string descricao) : base( nome, email, telefone,senha ) 
        {
            Descricao = descricao;
        }

        
    }
}

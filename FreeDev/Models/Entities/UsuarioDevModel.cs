using System.ComponentModel.DataAnnotations;

namespace FreeDev.Models.Entities
{
    public class UsuarioDevModel : UsuarioModel
    {

        [Display(Name= " Descrição")]
        public string Descricao { get; set; }
        public double Nota { get { return Avaliacoes.Average(); } }
        public List<Double> Avaliacoes { get; set; } = new List<double>();
        public UsuarioDevModel()
        {

        }

        public UsuarioDevModel(string nome,string email,string telefone,string senha, string descricao) : base(nome, email, telefone,senha ) 
        {
            Descricao = descricao;
        }

        
    }
}

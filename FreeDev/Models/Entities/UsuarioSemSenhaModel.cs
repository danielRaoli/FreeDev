using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace FreeDev.Models.Entities
{
    public class UsuarioSemSenhaModel
    {
        public int Id { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Preencha esse campo obrigatório")]
        public string Nome { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Preencha esse campo obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Digite um email válido")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Preencha esse campo obrigatório")]
        public string Telefone { get; set; }

        public UsuarioSemSenhaModel()
        {

        }

        public UsuarioSemSenhaModel(string nome, string email, string telefone)
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
        }
    }
}

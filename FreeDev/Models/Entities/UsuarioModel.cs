using FreeDev.Helpers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;

namespace FreeDev.Models.Entities
{
    public class UsuarioModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Display(Name="Nome")]
        [Required(ErrorMessage = "Preencha esse campo obrigatório")]
        public string Nome { get; set; }

        [Display(Name="Email")]
        [Required(ErrorMessage ="Preencha esse campo obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage ="Digite um email válido")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name ="Telefone")]
        [Required(ErrorMessage = "Preencha esse campo obrigatório")]
        public string Telefone { get; set; }

        
        [Display(Name= "Senha")]
        [Required(ErrorMessage = "Preencha esse campo obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime DataCriacao { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public UsuarioModel()
        {

        }

        public UsuarioModel(string nome,string email,string telefone, string senha )
        {
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Senha = senha;
        }

        public bool ValidaSenha(string senha)
        {
            return Senha.Equals(senha.GerarHash());
        }

        public void SetSenha()
        {
            Senha = Senha.GerarHash();
        }



    }
}

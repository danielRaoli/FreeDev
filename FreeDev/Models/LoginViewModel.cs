using System.ComponentModel.DataAnnotations;

namespace FreeDev.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "Preencha esse campo obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Digite um email válido")]
        public string Email { get; set; }
        [Display(Name = "Senha")]
        [Required(ErrorMessage = "Preencha esse campo obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}

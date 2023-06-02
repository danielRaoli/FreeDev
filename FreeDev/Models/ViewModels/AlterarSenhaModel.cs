using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace FreeDev.Models.ViewModels
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }
        [Display(Name = "Senha Atual")]
        [Required(ErrorMessage ="Preencha esse campo obrigatório")]
        [DataType(DataType.Password)]
        public string SenhaAtual { get; set; }

        [Display(Name = "Nova Senha")]
        [Required(ErrorMessage = "Preencha esse campo obrigatório")]
        [DataType(DataType.Password)]
        public string NovaSenha { get; set; }

        [Display(Name = "Confirmar nova senha")]
        [Required(ErrorMessage = "Preencha esse campo obrigatório")]
        [DataType(DataType.Password)]
        public string ConfirmarSenha { get; set; }
    }
}

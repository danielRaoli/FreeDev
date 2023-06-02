using FreeDev.Models.Entities;

namespace FreeDev.Models.ViewModels
{
    public class AlterarFoto
    {
        public IFormFile Arquivo { get; set; }
        public UsuarioDevSemSenhaModel UsuarioDev { get; set; }
    }
}

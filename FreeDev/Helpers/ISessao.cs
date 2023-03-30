using FreeDev.Models.Entities;

namespace FreeDev.Helpers
{
    public interface ISessao
    {
        void CriarSessao(UsuarioModel usuario);

        void DeletarSessao();

        UsuarioModel BuscarSessaoNormal();

        UsuarioDevModel BuscarSessaoDev();
    }
}

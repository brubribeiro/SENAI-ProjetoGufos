using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;

namespace backend.Interfaces
{
    public interface ITipoUsuario
    {
         Task<List<Tipousuario>> Listar();

         Task<Tipousuario> BuscarPorID(int id);

         Task<Tipousuario> Salvar(Tipousuario tipousuario);

         Task<Tipousuario> Alterar(Tipousuario tipousuario);

         Task<Tipousuario> Excluir(Tipousuario tipousuario);
    }
}
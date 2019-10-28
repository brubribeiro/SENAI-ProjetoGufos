using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class TipoUsuarioRepository : ITipoUsuario
    {
         public async Task<Tipousuario> Alterar(Tipousuario tipousuario)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                //Comparamos os atributos que foram modificados através do EF
                _contexto.Entry(tipousuario).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
                return tipousuario;
            }
        }

        public async Task<Tipousuario> BuscarPorID(int id)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                return await _contexto.Tipousuario.FindAsync(id);
            }
        }

        public async Task<Tipousuario> Excluir(Tipousuario tipousuario)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                _contexto.Tipousuario.Remove(tipousuario);
                await _contexto.SaveChangesAsync();
                return tipousuario;
            }
        }

        public async Task<List<Tipousuario>> Listar()
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                return await _contexto.Tipousuario.ToListAsync();
            }
        }

        public async Task<Tipousuario> Salvar(Tipousuario tipousuario)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(tipousuario);
                // Salvamos efetivamente o nosso objeto no banco
                await _contexto.SaveChangesAsync();
                return tipousuario;
            }
        }
    }
}
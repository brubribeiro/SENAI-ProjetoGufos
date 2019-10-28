using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        public async Task<Usuario> Alterar(Usuario usuario)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                //Comparamos os atributos que foram modificados através do EF
                _contexto.Entry(usuario).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
                return usuario;
            }
        }

        public async Task<Usuario> BuscarPorID(int id)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                return await _contexto.Usuario.Include("IdTipoUsuarioNavigation").FirstOrDefaultAsync(t => t.IdTipoUsuario == id);
            }
        }

        public async Task<Usuario> Excluir(Usuario usuario)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                _contexto.Usuario.Remove(usuario);
                await _contexto.SaveChangesAsync();
                return usuario;
            }
        }

        public async Task<List<Usuario>> Listar()
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                return await _contexto.Usuario.Include("IdTipoUsuarioNavigation").ToListAsync();
            }
        }

        public async Task<Usuario> Salvar(Usuario usuario)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(usuario);
                // Salvamos efetivamente o nosso objeto no banco
                await _contexto.SaveChangesAsync();
                return usuario;
            }
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class PresencaRepository : IPresenca
    {
        public async Task<Presenca> Alterar(Presenca presenca)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                //Comparamos os atributos que foram modificados através do EF
                _contexto.Entry(presenca).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
                return presenca;
            }
        }

        public async Task<Presenca> BuscarPorID(int id)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                return await _contexto.Presenca.Include("IdUsuarioNavigation").Include("IdEventoNavigation").FirstOrDefaultAsync(p => p.IdPresenca == id);;
            }
        }

        public async Task<Presenca> Excluir(Presenca presenca)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                _contexto.Presenca.Remove(presenca);
                await _contexto.SaveChangesAsync();
                return presenca;
            }
        }

        public async Task<List<Presenca>> Listar()
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                return await _contexto.Presenca.Include("IdUsuarioNavigation").Include("IdEventoNavigation").ToListAsync();
            }
        }

        public async Task<Presenca> Salvar(Presenca presenca)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(presenca);
                // Salvamos efetivamente o nosso objeto no banco
                await _contexto.SaveChangesAsync();
                return presenca;
            }
        }
    }
}
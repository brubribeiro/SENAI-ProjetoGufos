using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class EventoRepository : IEvento
    {
        
        public async Task<Evento> Alterar(Evento evento)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                //Comparamos os atributos que foram modificados através do EF
                _contexto.Entry(evento).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
                return evento;
            }
        }

        public async Task<Evento> BuscarPorID(int id)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                return await _contexto.Evento.Include("IdCategoriaNavigation").Include("IdLocalNavigation").FirstOrDefaultAsync(e => e.IdEvento == id);
            }
        }

        public async Task<Evento> Excluir(Evento evento)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                _contexto.Evento.Remove(evento);
                await _contexto.SaveChangesAsync();
                return evento;
            }
        }

        public async Task<List<Evento>> Listar()
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                return await _contexto.Evento.Include("IdCategoriaNavigation").Include("IdLocalNavigation").ToListAsync();
            }
        }

        public async Task<Evento> Salvar(Evento evento)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(evento);
                // Salvamos efetivamente o nosso objeto no banco
                await _contexto.SaveChangesAsync();
                return evento;
            }
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class LocalizacaoRepository : ILocalizacao
    {
        
        public async Task<Localizacao> Alterar(Localizacao localizacao)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                //Comparamos os atributos que foram modificados através do EF
                _contexto.Entry(localizacao).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
                return localizacao;
            }
        }

        public async Task<Localizacao> BuscarPorID(int id)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                return await _contexto.Localizacao.FindAsync(id);
            }
        }

        public async Task<Localizacao> Excluir(Localizacao localizacao)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                _contexto.Localizacao.Remove(localizacao);
                await _contexto.SaveChangesAsync();
                return localizacao;
            }
        }

        public async Task<List<Localizacao>> Listar()
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                return await _contexto.Localizacao.ToListAsync();
            }
        }

        public async Task<Localizacao> Salvar(Localizacao localizacao)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(localizacao);
                // Salvamos efetivamente o nosso objeto no banco
                await _contexto.SaveChangesAsync();
                return localizacao;
            }
        }
    }
}
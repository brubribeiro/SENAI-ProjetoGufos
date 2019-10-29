using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace backend.Repositories
{
    public class CategoriaRepository : ICategoria
    {
        public async Task<Categoria> Alterar(Categoria categoria)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                //Comparamos os atributos que foram modificados através do EF
                _contexto.Entry(categoria).State = EntityState.Modified;
                await _contexto.SaveChangesAsync();
                return categoria;
            }
        }

        public async Task<Categoria> BuscarPorID(int id)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                return await _contexto.Categoria.FindAsync(id);
            }
        }

        public async Task<Categoria> Excluir(Categoria categoria)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                _contexto.Categoria.Remove(categoria);
                await _contexto.SaveChangesAsync();
                return categoria;
            }
        }

        public async Task<List<Categoria>> Listar()
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                return await _contexto.Categoria.ToListAsync();
            }
        }

        public async Task<Categoria> Salvar(Categoria categoria)
        {
            using(bdgufosContext _contexto = new bdgufosContext()){
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(categoria);
                // Salvamos efetivamente o nosso objeto no banco
                await _contexto.SaveChangesAsync();
                return categoria;
            }
        }
    }
}
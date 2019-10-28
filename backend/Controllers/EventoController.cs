using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Para adicionar a árvore de objetos adicionamos uma nova biblioteca JSON
// dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson

namespace backend.Controllers
{
    // Definimos nossa rota do controller e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class EventoController: ControllerBase
    {
        EventoRepository _repositorio = new EventoRepository();

        //GET: api/Evento
        /// <summary>
        /// Pegamos todos os eventos cadastrados
        /// </summary>
        /// <returns>Lista de eventos</returns>
        [HttpGet]
        public async Task<ActionResult<List<Evento>>> Get()
        {
            var eventos = await _repositorio.Listar();
            
            if(eventos == null){
                return NotFound();
            }
            return eventos;
        }
        //GET: api/Evento/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Evento>> Get(int id)
        {
            var evento = await _repositorio.BuscarPorID(id);
            
            if(evento == null){
                return NotFound();
            }
            return evento;
        }

        [HttpPost]
        public async Task<ActionResult<Evento>> Post(Evento evento){
            try{
                await _repositorio.Salvar(evento);
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            return evento;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Evento evento){
            // Se o ID do objeto não existir, ele retorna o erro 400
            if(id != evento.IdEvento){
                return BadRequest();
            }
            
            try{
                await _repositorio.Alterar(evento);
            }catch(DbUpdateConcurrencyException){
                // Verificamos se o objeto inserido realmente existe no banco
                var evento_valido = await _repositorio.BuscarPorID(id);

                if(evento_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }
            // NoContent = Retorna 204, sem nada
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Evento>> Delete(int id){
            var evento = await _repositorio.BuscarPorID(id);
            if(evento == null){
                return NotFound();
            }
            await _repositorio.Excluir(evento);

            return evento;
        }
    }
}
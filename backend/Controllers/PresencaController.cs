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
    public class PresencaController: ControllerBase
    {
        //bdgufosContext _contexto = new bdgufosContext();
        PresencaRepository _repositorio = new PresencaRepository();

        //GET: api/Presenca
        [HttpGet]
        public async Task<ActionResult<List<Presenca>>> Get()
        {
            var presencas = await _repositorio.Listar();
            
            if(presencas == null){
                return NotFound();
            }
            return presencas;
        }
        //GET: api/Presenca/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Presenca>> Get(int id)
        {
            var presenca = await _repositorio.BuscarPorID(id);
            
            if(presenca == null){
                return NotFound();
            }
            return presenca;
        }

        [HttpPost]
        public async Task<ActionResult<Presenca>> Post(Presenca presenca){
            try{
                await _repositorio.Salvar(presenca);
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            return presenca;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Presenca presenca){
            // Se o ID do objeto não existir, ele retorna o erro 400
            if(id != presenca.IdPresenca){
                return BadRequest();
            }
            try{
               await _repositorio.Alterar(presenca);
            }catch(DbUpdateConcurrencyException){
                // Verificamos se o objeto inserido realmente existe no banco
                var presenca_valido = await _repositorio.BuscarPorID(id);

                if(presenca_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }
            // NoContent = Retorna 204, sem nada
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Presenca>> Delete(int id){
            var presenca = await _repositorio.BuscarPorID(id);
            if(presenca == null){
                return NotFound();
            }

            await _repositorio.Excluir(presenca);

            return presenca;
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Para adicionar a árvore de objetos adicionamos uma nova biblioteca JSON
// dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson

namespace backend.Controllers
{
    // Definimos nossa rota do controller e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class TipousuarioController: ControllerBase
    {
        bdgufosContext _contexto = new bdgufosContext();

        //GET: api/Tiposuario
        [HttpGet]
        public async Task<ActionResult<List<Tipousuario>>> Get()
        {
            var tipousuarios = await _contexto.Tipousuario.ToListAsync();
            
            if(tipousuarios == null){
                return NotFound();
            }
            return tipousuarios;
        }
        //GET: api/Tiposuario/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Tipousuario>> Get(int id)
        {
            var tipousuario = await _contexto.Tipousuario.FindAsync(id);
            
            if(tipousuario == null){
                return NotFound();
            }
            return tipousuario;
        }

        [HttpPost]
        public async Task<ActionResult<Tipousuario>> Post(Tipousuario tipousuario){
            try{
                // Tratamos contra ataques de SQL Injection
                await _contexto.AddAsync(tipousuario);
                // Salvamos efetivamente o nosso objeto no banco
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            return tipousuario;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Tipousuario tipousuario){
            // Se o ID do objeto não existir, ele retorna o erro 400
            if(id != tipousuario.IdTipoUsuario){
                return BadRequest();
            }
            //Comparamos os atributos que foram modificados através do EF
            _contexto.Entry(tipousuario).State = EntityState.Modified;

            try{
                await _contexto.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException){
                // Verificamos se o objeto inserido realmente existe no banco
                var tiposuario_valido = await _contexto.Tipousuario.FindAsync(id);

                if(tiposuario_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }
            // NoContent = Retorna 204, sem nada
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Tipousuario>> Delete(int id){
            var tipousuario = await _contexto.Tipousuario.FindAsync(id);
            if(tipousuario == null){
                return NotFound();
            }

            _contexto.Tipousuario.Remove(tipousuario);
            await _contexto.SaveChangesAsync();

            return tipousuario;
        }
    }
}
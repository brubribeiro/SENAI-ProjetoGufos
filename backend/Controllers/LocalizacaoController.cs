using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Domains;
using backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers
{
    //Definimos nossa rota do controller e dizemos que é um controller de API
    [Route("api/[controller]")]
    [ApiController]
    public class LocalizacaoController : ControllerBase
    {
        //bdgufosContext _contexto = new bdgufosContext();

        LocalizacaoRepository _repositorio = new LocalizacaoRepository();

        // GET: api/Localizacao
        [HttpGet]
        public async Task<ActionResult<List<Localizacao>>> Get()
        {
            var localizacoes = await _repositorio.Listar();

            if(localizacoes == null){
                return NotFound();
            }

            return localizacoes;
        }
        // GET: api/Localizacao/2
        [HttpGet("{id}")]
        public async Task<ActionResult<Localizacao>> Get(int id)
        {
            var localizacao = await _repositorio.BuscarPorID(id);

            if(localizacao == null){
                return NotFound();
            }

            return localizacao;
        }

        //
        //POST api/Localizacao
        [HttpPost]
        public async Task<ActionResult<Localizacao>> Post(Localizacao localizacao){
            try{
                await _repositorio.Salvar(localizacao);
            }catch(DbUpdateConcurrencyException){
                throw;
            }
            return localizacao;
        }

        //Update
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Localizacao localizacao){
            // Se o Id do objeto não existir, ele retorna erro 400
            if(id != localizacao.IdLocal){
                return BadRequest();
            }
            try{
                await _repositorio.Alterar(localizacao);
            }catch(DbUpdateConcurrencyException){
                // Verificamos se o objeto inserido realmente existe no banco
                var localizacao_valido = await _repositorio.BuscarPorID(id);

                if(localizacao_valido == null){
                    return NotFound();
                }else{
                    throw;
                }
            }
            // NoContent = Retorna 204, sem nada
            return NoContent();
        }

        //DELETE api/localizacao/id
        [HttpDelete("{id}")]
        public async Task<ActionResult<Localizacao>> Delete(int id){
            var localizacao = await _repositorio.BuscarPorID(id);
            if(localizacao == null){
                return NotFound();
            }

            await _repositorio.Excluir(localizacao);

            return localizacao;
        }
    }
}
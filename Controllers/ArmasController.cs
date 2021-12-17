using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RpgApi.Data;
using RpgApi.Models;

namespace RpgApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ArmasController : ControllerBase
    {
       private readonly DataContext _context; /*conteudo*/ /*varialvel*/ //Declaração

        public ArmasController(DataContext context)
        {   

            _context = context; //Inicialização do atributo

        }

        [HttpGet("{id}")] //Buscar pelo id
        public async Task<IActionResult> GetSingle(int id)//async = executar mais de um comando
        {
            //Programação será feita aqui
            try //Tentativa
            {
                Arma p = await _context.Armas.FirstOrDefaultAsync(pBusca => pBusca.Id == id);//await = só executara a função quando receber o seu conteudo

                return Ok(p);
            }
            catch (System.Exception ex)// ex = variavel que vai receber o erro
            {
                return BadRequest(ex.Message);
            }
        }  

        [HttpGet("GetAll")]

        public async Task<IActionResult> Get()
        {
            try
            {
                 List<Arma> lista = await _context.Armas.ToListAsync();
                 return Ok(lista);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                Arma pRemover = await _context.Armas.FirstOrDefaultAsync(p => p.Id == id); 
                _context.Armas.Remove(pRemover);

                int linhasAfetadas = await _context.SaveChangesAsync();

                return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [HttpPost]

        public async Task<IActionResult> Add(Arma novaArma)
        {
            try
            {
                if (novaArma.Dano == 0)
                    throw new System.Exception("O dano da arma não pode ser 0");

                Personagem p = await _context.Personagens
                    .FirstOrDefaultAsync(p => p.Id == novaArma.PersonagemId);

                if(p == null)
                    throw new System.Exception("Não existe personagem com Id informado neste usuario.");

                Arma buscaArma = await _context.Armas
                    .FirstOrDefaultAsync(a => a.PersonagemId == novaArma.PersonagemId);

                if(buscaArma != null)
                    throw new System.Exception("Já exite uma arma vinculado com neste personagem.");

                                 
                 await _context.Armas.AddAsync(novaArma);
                 await _context.SaveChangesAsync();

                 return Ok(novaArma.Id);
            }
            catch (System.Exception ex)
            {
                   
               return BadRequest(ex.Message);
  
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(Arma novaArma)
        {
            try
            {
                if (novaArma.Dano == 0)
                {
                    throw new System.Exception("O dano da arma não pode ser 0");
                }

                _context.Armas.Update(novaArma);
                int linhaAfetadas = await _context.SaveChangesAsync();

                return Ok(linhaAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
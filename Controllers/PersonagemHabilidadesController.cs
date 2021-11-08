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
    public class PersonagemHabilidadesController : ControllerBase
    {
        private readonly DataContext _context;
        public PersonagemHabilidadesController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> AddPersonagemHabilidadeAsync(PersonagemHabilidade novoPersonagemHabilidade)
        {
            try
            {
                Personagem personagem = await _context.Personagens
                    .Include(p => p.Arma)
                    .Include(p => p.PersonagemHabilidades).ThenInclude(ps => ps.Habilidade)
                    .FirstOrDefaultAsync(p => p.Id == novoPersonagemHabilidade.PersonagemId);
                    
                if (personagem == null)
                    throw new System.Exception("Personagem não encontrado para o Id Informado.");

                Habilidade habilidade = await _context.Habilidades
                                    .FirstOrDefaultAsync(h => h.Id == novoPersonagemHabilidade.HabilidadeId);

                if (habilidade == null)
                    throw new System.Exception("Habilidade não encontrada.");

                PersonagemHabilidade ph = new PersonagemHabilidade();

                ph.Personagem = personagem;
                ph.Habilidade = habilidade;
                await _context.PersonagemHabilidades.AddAsync(ph);
                int linhasAfetadas = await _context.SaveChangesAsync();
                return Ok(linhasAfetadas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("DeletePersonagemHabilidade")]
        public async Task<IActionResult> DeleteAsync(PersonagemHabilidade ph)
        {
             try
            {
             
                
                PersonagemHabilidade phRemover = await _context.PersonagemHabilidades
                    .FirstOrDefaultAsync(phBusca => phBusca.PersonagemId == ph.PersonagemId && phBusca.HabilidadeId == ph.HabilidadeId);

                if(phRemover == null)
                    throw new System.Exception("Personagem ou Habilidade não encontrados");
         
                
                _context.PersonagemHabilidades.Remove(phRemover);
                int linhasAfetadas = await _context.SaveChangesAsync();
                return Ok(linhasAfetadas);

            }
                catch (System.Exception ex)
                {
                    
                    return BadRequest(ex.Message);
                }
        }

     [HttpGet("{personagemid}")]
        public async Task<IActionResult> PersonagemGet(int personagemid)
        {
            try
            {
                List<PersonagemHabilidade> phLista =  new List<PersonagemHabilidade>();

                phLista = await _context.PersonagemHabilidades
                    .Include(p => p.Personagem)
                    .Include(h => h.Habilidade)
                    .Where(p => p.PersonagemId == personagemid).ToListAsync();
                return Ok(phLista);
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
     [HttpGet("GetAllHabilidades")]
        public async Task<IActionResult> GetAllHabilidades()
        {
            try
            {
                List<Habilidade> habilidades = new List<Habilidade>();
                habilidades =  await _context.Habilidades.ToListAsync();

                return Ok(habilidades);
            }
            catch(System.Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        
    }
}
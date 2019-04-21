using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMRCG.Contexts;
using WebApiMRCG.Entities;

namespace WebApiMRCG.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PecuaristasController : ControllerBase
    {
        private readonly Contexto _context;

        public PecuaristasController(Contexto context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter uma lista de pecuarista
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pecuarista>>> GetPecuaristas()
        {
            return await _context.Pecuaristas.ToListAsync();
        }

        /// <summary>
        /// Obter um pecuarista específico
        /// </summary>
        /// <param name="id">Id do pecuarista a obter</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Pecuarista>> GetPecuarista(int id)
        {
            var pecuarista = await _context.Pecuaristas.FindAsync(id);

            if (pecuarista == null)
            {
                return NotFound();
            }

            return pecuarista;
        }

        /// <summary>
        /// Atualizar um pecuarista específico
        /// </summary>
        /// <param name="id">Id do pecuarista a atualizar</param>
        /// <param name="pecuarista">Nome do parametro do pecuarista a atualizar</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPecuarista(int id, Pecuarista pecuarista)
        {
            if (id != pecuarista.PecuaristaId)
            {
                return BadRequest();
            }

            _context.Entry(pecuarista).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PecuaristaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// Adicionar um novo pecuarista
        /// </summary>
        /// <param name="pecuarista">Nome do parametro do pecuarista a adicionar</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Pecuarista>> PostPecuarista(Pecuarista pecuarista)
        {
            _context.Pecuaristas.Add(pecuarista);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPecuarista", new { id = pecuarista.PecuaristaId }, pecuarista);
        }

        /// <summary>
        /// Excluir um pecuarista específico
        /// </summary>
        /// <param name="id">Id do pecuarista a excluir</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Pecuarista>> DeletePecuarista(int id)
        {
            var pecuarista = await _context.Pecuaristas.FindAsync(id);
            if (pecuarista == null)
            {
                return NotFound();
            }

            _context.Pecuaristas.Remove(pecuarista);
            await _context.SaveChangesAsync();

            return pecuarista;
        }

        private bool PecuaristaExists(int id)
        {
            return _context.Pecuaristas.Any(e => e.PecuaristaId == id);
        }
    }
}

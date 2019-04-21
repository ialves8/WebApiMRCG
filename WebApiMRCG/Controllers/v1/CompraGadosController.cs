using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiMRCG.Contexts;
using WebApiMRCG.Entities;

namespace WebApiMRCG.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [EnableCors("PermitirApiRequest")]
    public class CompraGadosController : ControllerBase
    {
        private readonly Contexto _context;

        public CompraGadosController(Contexto context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter uma lista de compra
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompraGado>>> GetCompraGados(int PageNumber = 1, int RecordsMount = 10)
        {
            var Query = _context.CompraGados.AsQueryable();

            var TotalRecords = Query.Count();
                       
            var _getCompraGados = await Query
                .Skip(RecordsMount * (PageNumber - 1))
                .Take(RecordsMount)
                .Include(c => c.Pecuarista).ToListAsync();

            Response.Headers["X-Total-Registros"] = TotalRecords.ToString();
            Response.Headers["X-Quantidade-Paginas"] =
                ((int)Math.Ceiling((double)TotalRecords / RecordsMount)).ToString();

            return _getCompraGados;
        }

        /// <summary>
        /// Obter uma compra específico
        /// </summary>
        /// <param name="id">Id da compra a obter</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CompraGado>> GetCompraGado(int id)
        {
            var compraGado = await _context.CompraGados.FindAsync(id);

            if (compraGado == null)
            {
                return NotFound();
            }

            return compraGado;
        }

        /// <summary>
        /// Atualizar uma compra específico
        /// </summary>
        /// <param name="id">Id da compra a atualizar</param>
        /// <param name="compraGado">Nome do parametro da compra a atualizar</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompraGado(int id, CompraGado compraGado)
        {
            if (id != compraGado.CompraGadoId)
            {
                return BadRequest();
            }

            _context.Entry(compraGado).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraGadoExists(id))
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
        /// Adicionar uma nova compra
        /// </summary>
        /// <param name="compraGado">Nome do parametro da compra a adicionar</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CompraGado>> PostCompraGado(CompraGado compraGado)
        {
            _context.CompraGados.Add(compraGado);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompraGado", new { id = compraGado.CompraGadoId }, compraGado);
        }

        /// <summary>
        /// Excluir uma compra específica
        /// </summary>
        /// <param name="id">Id da compra a excluir</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CompraGado>> DeleteCompraGado(int id)
        {
            var compraGado = await _context.CompraGados.FindAsync(id);
            if (compraGado == null)
            {
                return NotFound();
            }

            _context.CompraGados.Remove(compraGado);
            await _context.SaveChangesAsync();

            return compraGado;
        }

        private bool CompraGadoExists(int id)
        {
            return _context.CompraGados.Any(e => e.CompraGadoId == id);
        }
    }
}

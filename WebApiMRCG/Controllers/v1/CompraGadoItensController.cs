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
    public class CompraGadoItensController : ControllerBase
    {
        private readonly Contexto _context;

        public CompraGadoItensController(Contexto context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter uma lista de itens da compra
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompraGadoItem>>> GetCompraGadoItens()
        {
            return await _context.CompraGadoItens.Include(c => c.Animal).Include(c => c.CompraGado).ToListAsync();
        }

        /// <summary>
        /// Obter um item da compra específico
        /// </summary>
        /// <param name="id">Id do item da compra a obter</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CompraGadoItem>> GetCompraGadoItem(int id)
        {
            var compraGadoItem = await _context.CompraGadoItens.FindAsync(id);

            if (compraGadoItem == null)
            {
                return NotFound();
            }

            return compraGadoItem;
        }

        /// <summary>
        /// Atualizar um item da compra específico
        /// </summary>
        /// <param name="id">Id do item da compra a atualizar</param>
        /// <param name="compraGadoItem">Nome do parametro do item da compra a atualizar</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompraGadoItem(int id, CompraGadoItem compraGadoItem)
        {
            if (id != compraGadoItem.CompraGadoItemId)
            {
                return BadRequest();
            }

            _context.Entry(compraGadoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompraGadoItemExists(id))
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
        /// Adicionar um novo item a compra
        /// </summary>
        /// <param name="compraGadoItem">Nome do parametro do item da compra a adcionar</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CompraGadoItem>> PostCompraGadoItem(CompraGadoItem compraGadoItem)
        {
            _context.CompraGadoItens.Add(compraGadoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompraGadoItem", new { id = compraGadoItem.CompraGadoItemId }, compraGadoItem);
        }

        /// <summary>
        /// Excluir um item da compra específico
        /// </summary>
        /// <param name="id">Id do item da compra a excluir</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<CompraGadoItem>> DeleteCompraGadoItem(int id)
        {
            var compraGadoItem = await _context.CompraGadoItens.FindAsync(id);
            if (compraGadoItem == null)
            {
                return NotFound();
            }

            _context.CompraGadoItens.Remove(compraGadoItem);
            await _context.SaveChangesAsync();

            return compraGadoItem;
        }

        private bool CompraGadoItemExists(int id)
        {
            return _context.CompraGadoItens.Any(e => e.CompraGadoItemId == id);
        }
    }
}

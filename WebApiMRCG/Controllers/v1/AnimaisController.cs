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
    public class AnimaisController : ControllerBase
    {
        private readonly Contexto _context;

        public AnimaisController(Contexto context)
        {
            _context = context;
        }

        /// <summary>
        /// Obter uma lista de animais
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimais()
        {
            return await _context.Animais.ToListAsync();
        }

        /// <summary>
        /// Obter um animal específico
        /// </summary>
        /// <param name="id">Id do animal a obter</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _context.Animais.FindAsync(id);

            if (animal == null)
            {
                return NotFound();
            }

            return animal;
        }

        /// <summary>
        /// Atualizar um animal específico
        /// </summary>
        /// <param name="id">Id do animal a atualizar</param>
        /// <param name="animal">Nome do parametro do animal a atualizar</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnimal(int id, Animal animal)
        {
            if (id != animal.AnimalId)
            {
                return BadRequest();
            }

            _context.Entry(animal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalExists(id))
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
        /// Adicionar um novo animal
        /// </summary>
        /// <param name="animal">Nome do parametro do animal a adicionar</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Animal>> PostAnimal(Animal animal)
        {
            _context.Animais.Add(animal);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnimal", new { id = animal.AnimalId }, animal);
        }

        /// <summary>
        /// Excluir um animal específico
        /// </summary>
        /// <param name="id">Id do animal a excluir</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Animal>> DeleteAnimal(int id)
        {
            var animal = await _context.Animais.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }

            _context.Animais.Remove(animal);
            await _context.SaveChangesAsync();

            return animal;
        }

        private bool AnimalExists(int id)
        {
            return _context.Animais.Any(e => e.AnimalId == id);
        }
    }
}

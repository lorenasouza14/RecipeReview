
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeReview.Classes;
using RecipeReview.Data;
using RecipeReview.Models;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredienteController : ControllerBase
    {
        private readonly DataContext _context;

        public IngredienteController(DataContext context)
        {
            _context = context;
        }

        // GET: api/<Ingredientes>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Ingrediente>>> GetIngredienteTable()
        { 
            return await _context.IngredienteTable.ToListAsync();
        }


        // GET api/<Ingredientes>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ingrediente>> GetIngrediente(int id)
        {
            var ingrediente = await _context.IngredienteTable.FindAsync(id);

            if (ingrediente == null)
            {
                return NotFound(new { Message = $"O Ingrediente com ID: {ingrediente.Id} não foi encontrado."});
            }

            return ingrediente;
        }

        // POST api/<Ingredientes>
        [HttpPost]
        public async Task<ActionResult<Ingrediente>> PostIngrediente(Ingrediente ingrediente)
        {
            _context.IngredienteTable.Add(ingrediente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAluno", new { id = ingrediente.Id }, ingrediente);
        }


        // PUT api/<Ingredientes>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, Ingrediente ingrediente)
        {
            if (id != ingrediente.Id)
            {
                return BadRequest();
            }

            _context.Entry(ingrediente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredienteExiste(id))
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


        // DELETE api/<Ingredientes>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngrediente(int id)
        {
            var ingrediente = await _context.IngredienteTable.FindAsync(id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            _context.IngredienteTable.Remove(ingrediente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredienteExiste(int id)
        {
            return _context.IngredienteTable.Any(e => e.Id == id);
        }
    }
}

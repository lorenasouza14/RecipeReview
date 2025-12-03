using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeReview.Data;
using RecipeReview.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecipeReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {

        private readonly DataContext _context;

        public UsuarioController(DataContext context)
        {
            _context = context;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetUsuarios()
        {
            return await _context.UsuarioTable
                .Include(u => u.ReceitasCriadas)
                .Include(u => u.Avaliacoes)
                .ToListAsync();
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuario(int id)
        {
            var user = await _context.UsuarioTable
                .Include(u => u.ReceitasCriadas)
                .Include(u => u.Avaliacoes)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound("Usuário não encontrado.");

            return user;
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            _context.UsuarioTable.Add(usuario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUsuario), new { id = usuario.Id }, usuario);
        }


        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id)
                return BadRequest();

            _context.Entry(usuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuario = await _context.UsuarioTable.FindAsync(id);

            if (usuario == null)
                return NotFound();

            _context.UsuarioTable.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

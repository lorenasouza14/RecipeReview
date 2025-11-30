using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeReview.Data;
using RecipeReview.Models;

namespace RecipeReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvaliacaoController : ControllerBase
    {
        private readonly DataContext _context;

        public AvaliacaoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/avaliacao
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var avaliacoes = await _context.AvaliacaoTable
                .Include(a => a.Usuario)
                .Include(a => a.Receita)
                .ToListAsync();

            return Ok(avaliacoes);
        }

        // GET: api/avaliacao/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var avaliacao = await _context.AvaliacaoTable
                .Include(a => a.Usuario)
                .Include(a => a.Receita)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (avaliacao == null)
                return NotFound(new { Message = $"Avaliação com Id={id} não encontrada." });

            return Ok(avaliacao);
        }

        // POST: api/avaliacao
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Avaliacao newAvaliacao)
        {
            if (newAvaliacao == null)
                return BadRequest("O corpo da requisição é inválido.");

            if (newAvaliacao.Nota < 1 || newAvaliacao.Nota > 5)
                return BadRequest("A nota deve ser entre 1 e 5.");

            var usuario = await _context.UsuarioTable.FindAsync(newAvaliacao.UsuarioId);
            if (usuario == null)
                return BadRequest($"Usuário com Id={newAvaliacao.UsuarioId} não encontrado.");

            var receita = await _context.ReceitaTable.FindAsync(newAvaliacao.ReceitaId);
            if (receita == null)
                return BadRequest($"Receita com Id={newAvaliacao.ReceitaId} não encontrada.");

            _context.AvaliacaoTable.Add(newAvaliacao);
            await _context.SaveChangesAsync();

            var createdAvaliacao = await _context.AvaliacaoTable
                .Include(a => a.Usuario)
                .Include(a => a.Receita)
                .FirstOrDefaultAsync(a => a.Id == newAvaliacao.Id);

            return CreatedAtAction(nameof(GetById), new { id = newAvaliacao.Id }, createdAvaliacao);
        }

        // PUT: api/avaliacao/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Avaliacao updatedAvaliacao)
        {
            if (updatedAvaliacao == null)
                return BadRequest("O corpo da requisição é inválido.");

            var existing = await _context.AvaliacaoTable.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null)
                return NotFound(new { Message = $"Avaliação com Id={id} não encontrada." });

            if (updatedAvaliacao.Nota < 1 || updatedAvaliacao.Nota > 5)
                return BadRequest("A nota deve ser entre 1 e 5.");

            var usuario = await _context.UsuarioTable.FindAsync(updatedAvaliacao.UsuarioId);
            if (usuario == null)
                return BadRequest($"Usuário com Id={updatedAvaliacao.UsuarioId} não encontrado.");

            var receita = await _context.ReceitaTable.FindAsync(updatedAvaliacao.ReceitaId);
            if (receita == null)
                return BadRequest($"Receita com Id={updatedAvaliacao.ReceitaId} não encontrada.");

            updatedAvaliacao.Id = existing.Id;

            _context.Entry(existing).CurrentValues.SetValues(updatedAvaliacao);
            await _context.SaveChangesAsync();

            var updated = await _context.AvaliacaoTable
                .Include(a => a.Usuario)
                .Include(a => a.Receita)
                .FirstOrDefaultAsync(a => a.Id == id);

            return Ok(new
            {
                Message = "Avaliação atualizada com sucesso.",
                Updated = updated
            });
        }

        
        // PATCH: api/avaliacao/5
        // Atualizar parcialmente (nota e/ou comentário)
      
        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] Avaliacao partialUpdate)
        {
            var avaliacao = await _context.AvaliacaoTable.FirstOrDefaultAsync(x => x.Id == id);

            if (avaliacao == null)
                return NotFound(new { Message = $"Avaliação com Id={id} não encontrada." });

            // Atualiza somente os campos preenchidos
            if (partialUpdate.Nota != 0)
            {
                if (partialUpdate.Nota < 1 || partialUpdate.Nota > 5)
                    return BadRequest("A nota deve ser entre 1 e 5.");

                avaliacao.Nota = partialUpdate.Nota;
            }

            if (!string.IsNullOrEmpty(partialUpdate.Comentario))
                avaliacao.Comentario = partialUpdate.Comentario;

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = "Avaliação atualizada parcialmente.",
                Updated = avaliacao
            });
        }

        // DELETE: api/avaliacao/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var avaliacao = await _context.AvaliacaoTable.FirstOrDefaultAsync(x => x.Id == id);
            if (avaliacao == null)
                return NotFound(new { Message = $"Avaliação com Id={id} não encontrada." });

            _context.AvaliacaoTable.Remove(avaliacao);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Avaliação removida com sucesso." });
        }
    }
}

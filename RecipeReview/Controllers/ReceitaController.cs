using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeReview.Data;
using RecipeReview.Models;

namespace RecipeReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        private readonly DataContext _context;

        public ReceitaController(DataContext context)
        {
            _context = context;
        }


        // GET: api/receita
 
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var receitas = await _context.ReceitaTable
                .Include(r => r.Usuario)
                .Include(r => r.Ingredientes)
                    .ThenInclude(ri => ri.Ingrediente)
                .ToListAsync();

            return Ok(receitas);
        }

      
        // GET: api/receita/5
   
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var receita = await _context.ReceitaTable
                .Include(r => r.Usuario)
                .Include(r => r.Ingredientes)
                    .ThenInclude(ri => ri.Ingrediente)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (receita == null)
                return NotFound(new { Message = $"Receita com Id={id} não encontrada." });

            return Ok(receita);
        }


        // POST: api/receita
  
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Receita novaReceita)
        {
            if (novaReceita == null)
                return BadRequest("O corpo da requisição é inválido.");

            var usuario = await _context.UsuarioTable.FindAsync(novaReceita.UsuarioId);
            if (usuario == null)
                return BadRequest($"Usuário com Id={novaReceita.UsuarioId} não encontrado.");

            // salva ingredientes enviados para usar depois
            var ingredientes = novaReceita.Ingredientes;

            // impede o EF de tentar inserir ingredientes automaticamente
            novaReceita.Ingredientes = null;

            // salva só a receita
            _context.ReceitaTable.Add(novaReceita);
            await _context.SaveChangesAsync();

            // agora adiciona a tabela N:N manualmente
            if (ingredientes != null)
            {
                foreach (var ing in ingredientes)
                {
                    var ingrediente = await _context.IngredienteTable.FindAsync(ing.IngredienteId);
                    if (ingrediente == null)
                        return BadRequest($"Ingrediente com Id={ing.IngredienteId} não encontrado.");

                    ing.ReceitaId = novaReceita.Id;
                    ing.Ingrediente = null; // importante
                    ing.Receita = null;     // importante

                    _context.ReceitasIngredientesTable.Add(ing);
                }

                await _context.SaveChangesAsync();
            }

            var criada = await _context.ReceitaTable
                .Include(r => r.Usuario)
                .Include(r => r.Ingredientes)
                    .ThenInclude(ri => ri.Ingrediente)
                .FirstOrDefaultAsync(r => r.Id == novaReceita.Id);

            return CreatedAtAction(nameof(GetById), new { id = novaReceita.Id }, criada);
        }


        // PUT: api/receita/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Receita receitaAtualizada)
        {
            if (receitaAtualizada == null)
                return BadRequest("O corpo da requisição é inválido.");

            var existente = await _context.ReceitaTable
                .Include(r => r.Ingredientes)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existente == null)
                return NotFound(new { Message = $"Receita com Id={id} não encontrada." });

            var usuario = await _context.UsuarioTable.FindAsync(receitaAtualizada.UsuarioId);
            if (usuario == null)
                return BadRequest($"Usuário com Id={receitaAtualizada.UsuarioId} não encontrado.");

            _context.Entry(existente).CurrentValues.SetValues(receitaAtualizada);

            await _context.SaveChangesAsync();

            var atualizada = await _context.ReceitaTable
                .Include(r => r.Usuario)
                .Include(r => r.Ingredientes)
                    .ThenInclude(ri => ri.Ingrediente)
                .FirstOrDefaultAsync(r => r.Id == id);

            return Ok(new
            {
                Message = "Receita atualizada com sucesso.",
                Updated = atualizada
            });
        }


        // PATCH: adicionar ingrediente
        // api/receita/5/ingredientes

        [HttpPatch("{id}/ingredientes")]
        public async Task<IActionResult> PatchAdicionarIngrediente(int id, [FromBody] ReceitaIngrediente novoIngrediente)
        {
            var receita = await _context.ReceitaTable
                .Include(r => r.Ingredientes)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (receita == null)
                return NotFound(new { Message = $"Receita com Id={id} não encontrada." });

            var ingrediente = await _context.IngredienteTable.FindAsync(novoIngrediente.IngredienteId);
            if (ingrediente == null)
                return BadRequest($"Ingrediente com Id={novoIngrediente.IngredienteId} não encontrado.");

            var novo = new ReceitaIngrediente
            {
                ReceitaId = id,
                IngredienteId = novoIngrediente.IngredienteId,
                Quantidade = novoIngrediente.Quantidade,
                Unidade = novoIngrediente.Unidade
            };

            receita.Ingredientes.Add(novo);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                Message = $"Ingrediente '{ingrediente.Nome}' adicionado à receita.",
                Updated = receita
            });
        }

     
        // DELETE: api/receita/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var receita = await _context.ReceitaTable.FirstOrDefaultAsync(x => x.Id == id);

            if (receita == null)
                return NotFound(new { Message = $"Receita com Id={id} não encontrada." });

            _context.ReceitaTable.Remove(receita);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Receita '{receita.Nome}' removida com sucesso." });
        }
    }
}

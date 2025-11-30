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
        private readonly RecipeService _recipeService; 

        public ReceitaController(DataContext context, RecipeService recipeService)
        {
            _context = context;
            _recipeService = recipeService;
        }

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

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Receita novaReceita)
        {
            if (novaReceita == null)
                return BadRequest("O corpo da requisição é inválido.");

            var usuario = await _context.UsuarioTable.FindAsync(novaReceita.UsuarioId);
            if (usuario == null)
                return BadRequest($"Usuário com Id={novaReceita.UsuarioId} não encontrado.");

            var ingredientes = novaReceita.Ingredientes;

            novaReceita.Ingredientes = null;

            _context.ReceitaTable.Add(novaReceita);
            await _context.SaveChangesAsync();

            if (ingredientes != null)
            {
                foreach (var ing in ingredientes)
                {
                    var ingrediente = await _context.IngredienteTable.FindAsync(ing.IngredienteId);
                    if (ingrediente == null)
                        return BadRequest($"Ingrediente com Id={ing.IngredienteId} não encontrado.");

                    ing.ReceitaId = novaReceita.Id;
                    ing.Ingrediente = null;
                    ing.Receita = null;

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


        [HttpGet("externo/ingrediente/{ingrediente}")]
        public async Task<IActionResult> BuscarExternoPorIngrediente(string ingrediente)
        {
            var lista = await _recipeService.BuscarPorIngrediente(ingrediente);
            return Ok(lista);
        }

        // GET: api/receita/externo/detalhes/52940
        [HttpGet("externo/detalhes/{id}")]
        public async Task<IActionResult> BuscarExternoDetalhes(string id)
        {
            var receita = await _recipeService.BuscarDetalhes(id);
            return Ok(receita);
        }
    }
}

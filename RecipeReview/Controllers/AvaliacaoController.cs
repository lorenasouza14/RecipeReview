using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeReview.Data;

namespace RecipeReview.Controllers
{
    public class AvaliacaoController : Controller
    {
        private readonly DataContext _context;

        public AvaliacaoController(DataContext context)
        {
            _context = context;
        }
        // GET: AvaliacaoController
        
        public async Task<IActionResult> Get()
        {
            var avalis = await _context.AvaliacaoTable
                .Include (t=> t.Receita)
                .ToListAsync();

            return Ok(avalis);
        }

        // GET: AvaliacaoController/Details/5
        public async Task<IActionResult> GetById(int id)
        {
            var avali = await _context.AvaliacaoTable
                .Include(t=> t.Receita)
                .FirstOrDefaultAsync(x=> x.Id == id);

            if (avali == null)
                return NotFound(new { Message = $"Tarefa com Id = {id} não encontrada" });

            return Ok(avali);
        }

        // POST: AvaliacaoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AvaliacaoController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AvaliacaoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AvaliacaoController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AvaliacaoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

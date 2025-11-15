using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipeReview.Controllers
{
    public class ReceitaSalgadaContoller : Controller
    {
        // GET: ReceitaSalgadaContoller
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReceitaSalgadaContoller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReceitaSalgadaContoller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReceitaSalgadaContoller/Create
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

        // GET: ReceitaSalgadaContoller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReceitaSalgadaContoller/Edit/5
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

        // GET: ReceitaSalgadaContoller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReceitaSalgadaContoller/Delete/5
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

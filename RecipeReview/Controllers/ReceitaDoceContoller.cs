using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipeReview.Controllers
{
    public class ReceitaDoceContoller : Controller
    {
        // GET: ReceitaDoceContoller
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReceitaDoceContoller/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReceitaDoceContoller/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReceitaDoceContoller/Create
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

        // GET: ReceitaDoceContoller/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReceitaDoceContoller/Edit/5
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

        // GET: ReceitaDoceContoller/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReceitaDoceContoller/Delete/5
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

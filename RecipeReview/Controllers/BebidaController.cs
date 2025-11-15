using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RecipeReview.Controllers
{
    public class BebidaController : Controller
    {
        // GET: BebidaController
        public ActionResult Index()
        {
            return View();
        }

        // GET: BebidaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BebidaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BebidaController/Create
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

        // GET: BebidaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BebidaController/Edit/5
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

        // GET: BebidaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BebidaController/Delete/5
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

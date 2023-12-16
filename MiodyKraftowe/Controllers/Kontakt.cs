using MiodyKraftowe.Data;
using MiodyKraftowe.Models;
using Microsoft.AspNetCore.Mvc;

namespace MiodyKraftowe.Controllers
{
    public class Kontakt : Controller
    {
        private readonly ApplicationDbContext _db;
        public Kontakt(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Wiadomosc> objProduktyList = _db.Wiadomosci;
            return View(objProduktyList);
        }

        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Gotowe()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Wiadomosc obj)
        {
            if (ModelState.IsValid)
            {
                _db.Wiadomosci.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Gotowe");
            }
            return View(obj);
        }

        public IActionResult Delete(Guid Id)
        {

            var postFromDb = _db.Wiadomosci.Find(Id);

            if (postFromDb == null)
            {
                return NotFound();
            }

            return View(postFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(Guid Id)
        {
            var obj = _db.Wiadomosci.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Wiadomosci.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}

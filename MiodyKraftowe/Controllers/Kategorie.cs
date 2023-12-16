using Microsoft.AspNetCore.Mvc;
using MiodyKraftowe.Models;
using MiodyKraftowe.Data;

namespace MiodyKraftowe.Controllers
{
    public class Kategorie : Controller
    {
        private readonly ApplicationDbContext _db;
        public Kategorie(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Kategoria> objCategoryList = _db.Kategorie;
            return View(objCategoryList);
        }

    public IActionResult Miód()
        {
            IEnumerable<Produkt> objCategoryList = _db.Produkty.Where(k=> k.NazwaKategoria.Contains("Miód")).ToList();
            return View(objCategoryList);


        }
        public IActionResult Pyłek()
        {
            IEnumerable<Produkt> objCategoryList = _db.Produkty.Where(k => k.NazwaKategoria.Contains("Pyłek")).ToList();
            return View(objCategoryList);
        }
        public IActionResult Artykuły()
        {
            IEnumerable<Produkt> objCategoryList = _db.Produkty.Where(k => k.NazwaKategoria.Contains("Artykuły")).ToList();    
                return View(objCategoryList);
         
        }

        public IActionResult KategorieAdmin()
        {
            IEnumerable<Kategoria> objCategoryList = _db.Kategorie;
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Kategoria obj)
        {
            if (obj.Nazwa == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Kategorie.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Kategorie.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Kategoria obj)
        {
            if (obj.Nazwa == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Kategorie.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _db.Kategorie.Find(id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.Kategorie.Find(id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Kategorie.Remove(obj);
            _db.SaveChanges();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");

        }




    }
}

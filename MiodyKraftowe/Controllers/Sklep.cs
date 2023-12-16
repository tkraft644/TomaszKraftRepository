using MiodyKraftowe.Data;
using MiodyKraftowe.Models;
using Microsoft.AspNetCore.Mvc;

namespace MiodyKraftowe.Controllers
{
    public class Sklep : Controller
    {
        private readonly ApplicationDbContext _db;

        public Sklep(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<CartItem> Koszyk = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new()
            {
                CartItems = Koszyk,
                GrandTotal = Koszyk.Sum(x => x.Quantity * x.Price)
            };

            IEnumerable<Produkt> objProduktyList = _db.Produkty;
            return View(objProduktyList);
        }
        public IActionResult Products()
        {
            IEnumerable<Produkt> objProduktyList = _db.Produkty;
            return View(objProduktyList);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Produkt obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Obrazek != null)
                {
                    obj.ObrazekURL += "/images/uploaded/" + obj.Id.ToString() + "_" + obj.Obrazek.FileName;
                    string folder = "wwwroot" + obj.ObrazekURL;
                    await obj.Obrazek.CopyToAsync(new FileStream(folder, FileMode.Create));
                }

                _db.Produkty.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Edit(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var produktFromDb = _db.Produkty.Find(Id);
            if (produktFromDb == null)
            {
                return NotFound();
            }
            return View(produktFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Produkt obj)
        {
            if (ModelState.IsValid)
            {
                _db.Produkty.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var produktFromDb = _db.Produkty.Find(Id);
            if (produktFromDb == null)
            {
                return NotFound();
            }
            return View(produktFromDb);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int Id)
        {
            var obj = _db.Produkty.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Produkty.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

using MiodyKraftowe.Data;
using MiodyKraftowe.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;



namespace MiodyKraftowe.Controllers
{
    public class Aktualności : Controller
    {
        private readonly ApplicationDbContext _db;
        public Aktualności(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Post> objPostyList = _db.Posty.OrderByDescending(x => x.DataPublikacji);
            return View(objPostyList);
        }
        public IActionResult Posts()
        {
            IEnumerable<Post> objPostyList = _db.Posty.OrderByDescending(x => x.DataPublikacji); ;
            return View(objPostyList);
        }

        public IActionResult CzytajDalej(Guid Id)
        {
            var postFromDb = _db.Posty.Find(Id);

            return View(postFromDb);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Post obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Obrazek != null)
                {
                    obj.ObrazekURL += "/images/uploaded/" + Guid.NewGuid().ToString() + "_" + obj.Obrazek.FileName;
                    string folder = "wwwroot" + obj.ObrazekURL;
                    await obj.Obrazek.CopyToAsync(new FileStream(folder, FileMode.Create));
                }

                _db.Posty.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var postFromDb = _db.Posty.Find(Id);

            if (postFromDb == null)
            {
                return NotFound();
            }

            return View(postFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Post obj)
        {       
            if (ModelState.IsValid)
            {
                _db.Posty.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var postFromDb = _db.Posty.Find(Id);

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
            var obj = _db.Posty.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }

            _db.Posty.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}

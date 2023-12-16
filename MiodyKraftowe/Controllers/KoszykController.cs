using Microsoft.AspNetCore.Mvc;
using MiodyKraftowe.Models;
using MiodyKraftowe.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MiodyKraftowe.Controllers
{
    public class KoszykController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KoszykController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<CartItem> Koszyk = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartViewModel cartVM = new()
            {
                CartItems = Koszyk,
                GrandTotal = Koszyk.Sum(x => x.Quantity * x.Price)
            };

            return View(cartVM);
        }

        public async Task<IActionResult> Add(int id)
        {
            Produkt product = await _context.Produkty.FindAsync(id);

            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem == null)
            {
                cart.Add(new CartItem(product));
            }
            else
            {
                cartItem.Quantity += 1;
            }

            HttpContext.Session.SetJson("Cart", cart);

            TempData["Success"] = "Produkt został dodany!";

            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> Decrease(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            CartItem cartItem = cart.Where(c => c.ProductId == id).FirstOrDefault();

            if (cartItem.Quantity > 1)
            {
                --cartItem.Quantity;
            }
            else
            {
                cart.RemoveAll(p => p.ProductId == id);
            }

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "Produkt został usunięty!";

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Remove(int id)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart");

            cart.RemoveAll(p => p.ProductId == id);

            if (cart.Count == 0)
            {
                HttpContext.Session.Remove("Cart");
            }
            else
            {
                HttpContext.Session.SetJson("Cart", cart);
            }

            TempData["Success"] = "Produkt został usunięty!";

            return RedirectToAction("Index");
        }

        public IActionResult Clear()
        {
            HttpContext.Session.Remove("Cart");

            return RedirectToAction("Index");
        }




        [HttpGet]
        public IActionResult Order()
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            OrderModel model = new OrderModel();
            foreach (var item in cart)
            {
                _context.OrdersDetail.Add(new OrderModelDetail
                {
                    Order = model,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Image = item.Image

                });
            }
            model.GrandTotal = cart.Sum(x => x.Total);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Order([Bind("Email,Adress,Number,Name,LastName,GrandTotal,CartItems")] OrderModel orderModel)
        {
            List<CartItem> cart = HttpContext.Session.GetJson<List<CartItem>>("Cart") ?? new List<CartItem>();

            OrderModel order = new OrderModel
            {
                OrderUserID = User.Identity.Name,
                Email = orderModel.Email,
                Adress = orderModel.Adress,
                Number = orderModel.Number,
                Name = orderModel.Name,
                LastName = orderModel.LastName,
                GrandTotal = orderModel.GrandTotal,
                Status = "Oczekujące"
            };
            foreach (var item in cart)
            {
                _context.OrdersDetail.Add(new OrderModelDetail
                {
                    Order = order,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    Image = item.Image

                });
            }
            if (order.Name != null && order.Number != null && order.Email != null) { 
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetJson("Cart", new List<CartItem>());

            return RedirectToAction("AllOrders");
            }return View(order);
        }

        public IActionResult AllOrders()
        {
            var orders = _context.Orders.ToList();

            return View(orders);
        }
        public IActionResult Details(int id)
        {
            var order = _context.Orders
                .Include(o => o.OrderModelDetails)
                .FirstOrDefault(o => o.OrderID == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return RedirectToAction("AdministracjaZamowien");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,Status,Email,Adress,Number,Name,LastName")] OrderModel order)
        {
            var oldOrder = await _context.Orders.FindAsync(id);

            order.OrderUserID = oldOrder.OrderUserID;
            order.OrderID = oldOrder.OrderID;
            order.GrandTotal = oldOrder.GrandTotal;
            order.OrderModelDetails = oldOrder.OrderModelDetails;


            var existingOrder = await _context.Orders.FindAsync(order.OrderID);

            try
            {
                _context.Update(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AdministracjaZamowien));
            }
            catch (InvalidOperationException)
            {
                _context.Entry(existingOrder).State = EntityState.Modified;

                _context.Entry(existingOrder).CurrentValues.SetValues(order);

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AdministracjaZamowien));
            }

            return View(order);
        }
        public IActionResult AdministracjaZamowien()
        {
            IEnumerable<OrderModel> objOrderList = _context.Orders;
            return View(objOrderList);
        }
        private bool OrderModelExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
        public IActionResult HistoriaZamowien()
        {
            IEnumerable<OrderModel> objOrderList = _context.Orders;
            return View(objOrderList);
        }
    }

}
#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Data;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ProgramContext _context;
        private readonly UserManager<IdentityUser> users;

        public OrdersController(ProgramContext context, UserManager<IdentityUser> user)
        {
            _context = context;
            users = user;
        }

        // GET: Orders
        /*public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }*/

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Order_date,State")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Order_date,State")] Order order)
        {
            if (id != order.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }

        // this is where we fake the order state progression
        private void hackyfunction(ProgramContext context)
        {
            const int chances = 1;
            if(Random.Shared.Next(chances) == 0)
            {
                context.Orders.Where(order => order.State == Order.Order_State.delivering).ForEachAsync(x => x.State = Order.Order_State.delivered);
                context.Orders.Where(order => order.State == Order.Order_State.paid_for).ForEachAsync(x => x.State = Order.Order_State.being_made);
                context.SaveChanges();
                //.Select(x => new Order { ID = x.ID, Order_date = x.Order_date, Payment_ID = x.Payment_ID});
            }
        }

        public ActionResult Index()
        {
            const int chances = 3;
            if (Random.Shared.Next(chances) == 0) // 1 is 5 sansas
            {
                var orders1 = _context.Orders.Where(order => order.State == Order.Order_State.delivering).AsEnumerable();
                foreach(var order in orders1)
                {
                    order.State = Order.Order_State.delivered;
                    _context.Orders.Update(order);
                }
                var orders4 = _context.Orders.Where(order => order.State == Order.Order_State.made).AsEnumerable();
                foreach (var order in orders4)
                {
                    order.State = Order.Order_State.delivered;
                    _context.Orders.Update(order);
                }
                var orders3 = _context.Orders.Where(order => order.State == Order.Order_State.being_made).AsEnumerable();
                foreach (var order in orders3)
                {
                    order.State = Order.Order_State.made;
                    _context.Orders.Update(order);
                }
                var orders2 = _context.Orders.Where(order => order.State == Order.Order_State.paid_for).AsEnumerable();
                foreach (var order in orders2)
                {
                    order.State = Order.Order_State.being_made;
                    _context.Orders.Update(order);
                }
                _context.SaveChanges();
                //.Select(x => new Order { ID = x.ID, Order_date = x.Order_date, Payment_ID = x.Payment_ID});
            }

            if (!User.IsInRole("Administrator")) 
            {// uzkomentavau nes nenutryniau carts
/*                if (User.IsInRole("RestaurantRepresentative"))
                {
                    // fix this
                    var user_id = users.Users.Where(x => x.UserName == User.Identity.Name)
                        .Select(x => x.Id).SingleOrDefault();
                    var rez_id = _context.Restaurants.Where(x => x.User_ID == user_id)
                        .Select(x => x.ID).SingleOrDefault();
                   // var carts = _context.Carts.Where(x => x.Rest_ID == rez_id).Select(x => x.ID);
                    var orders = _context.Orders.Where(x => carts.Contains(x.Cart_ID)).Select(x => x);
                    return View("Index",orders);
                }
                else
                {*/
                    var user_id = users.Users.Where(x => x.UserName == User.Identity.Name)
                        .Select(x => x.Id).SingleOrDefault();
                var orders = _context.Orders.Where(x => x.User_ID == user_id).OrderByDescending(x => x.Order_date);
                    return View("Index", orders); 
           //     }
            }
            else return View("Views/Shared/_LoginPartial");
        }

        public ActionResult OrderPayment(int id)
        {
           
            return View(_context.Orders.Where(order => order.ID == id).SingleOrDefault());
        }



        public ActionResult DoPayment(int id)
        {
            var payment = new Payment()
            { PaymentDate = DateTime.Now };
            _context.Payments.Add(payment);
            var order = _context.Orders.Where(order => order.ID == id).FirstOrDefault();
            order.Payment_ID = payment.ID;
            order.State = Order.Order_State.paid_for;
            _context.Orders.Update(order);
            _context.SaveChanges();

            var user_id = users.Users.Where(x => x.UserName == User.Identity.Name)
                .Select(x => x.Id).SingleOrDefault();
            var orders = _context.Orders.Where(x => x.User_ID == user_id);
            return View("Index", orders);
        }
    }
}

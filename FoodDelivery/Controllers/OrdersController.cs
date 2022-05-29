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
using static FoodDelivery.Models.OrderDetailViewModel;

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

            var ordered_meals = _context.OrderedMeals.Where(x => x.Order_ID == id).Select(x => x);
            var info = ordered_meals.Join(_context.Meals, oMeal => oMeal.Meal_ID, meal => meal.ID, (oMeal, meal) => new TMealList
            {
                Name = meal.Name,
                Amount = oMeal.Amount,
                Price = meal.Price
            });
            var totalPrice = Math.Round(info.Sum(x => x.Amount * x.Price),2);
            OrderDetailViewModel detail = new OrderDetailViewModel(info, totalPrice, order);
            return View(detail);
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

        public ActionResult Index()
        {
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
                var orders = _context.Orders.Where(x => x.User_ID == user_id);
                    return View("Index", orders); 
           //     }
            }
            else return View("Views/Shared/_LoginPartial");
        }

        public ActionResult OrderPayment(int id)
        {
           
            return View("Views/Shared/_LoginPartial");
        }
    }
}

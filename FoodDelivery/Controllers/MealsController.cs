#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Data;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;

using FoodDelivery.Controllers;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Controllers
{
    public class MealsController : Controller
    {
        private readonly ProgramContext _context;
        private readonly UserManager<IdentityUser> users;

        public MealsController(ProgramContext context, UserManager<IdentityUser> user)
        {
            _context = context;
            users = user;
        }

        // GET: Meals
        public async Task<IActionResult> Index()
        {
            ViewData["Restaurants"] = _context.Restaurants.Select(x => x);
            return View(await _context.Meals.ToListAsync());
        }

        // GET: Meals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals
                .FirstOrDefaultAsync(m => m.ID == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // GET: Meals/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Restaurant_ID,Order_ID,Name,Price,Description,Image_file_path")] Meal meal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(meal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(meal);
        }

        // GET: Meals/Edit/5
        [Authorize(Roles = "RestaurantRepresentative")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals.FindAsync(id);
            if (meal == null)
            {
                return NotFound();
            }
            return View(meal);
        }

        // POST: Meals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Restaurant_ID,Order_ID,Name,Price,Description,Image_file_path")] Meal meal)
        {
            if (id != meal.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(meal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealExists(meal.ID))
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
            return View(meal);
        }

        // GET: Meals/Delete/5
        [Authorize(Roles = "RestaurantRepresentative")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals
                .FirstOrDefaultAsync(m => m.ID == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View(meal);
        }

        // POST: Meals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            _context.Meals.Remove(meal);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealExists(int id)
        {
            return _context.Meals.Any(e => e.ID == id);
        }


        // GET: Meals/AddToOrder/5
        public async Task<IActionResult> AddToOrder(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var meal = await _context.Meals
                .FirstOrDefaultAsync(m => m.ID == id);
            if (meal == null)
            {
                return NotFound();
            }

            return View("/Views/Meals/Index.cshtml", await _context.Meals.ToListAsync());
        }


        // TO DO:
        // Modify user entity so that he'd have "currentOrderID".
        // Modify order entity so that they'd track IDs of all meals
        // WITHOUT the above mentioned, this feature won't be full implemented
        //
        //
        // POST: Meals/AddToOrder/5
        [HttpPost, ActionName("AddToOrder")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToOrderConfirmed(int id)
        {
            var meal = await _context.Meals.FindAsync(id);
            //_context.Orders.Add(meal);
            //await _context.SaveChangesAsync();
            return View("/Views/Meals/Edit.cshtml", await _context.Meals.ToListAsync());
        }

        private bool AlreadyInOrder(int id)
        {
            return _context.Meals.Any(e => e.ID == id);
        }
        //mano

        public ActionResult MealView(int id)
        {
            return View(_context.Meals.FirstOrDefault(m => m.ID == id));
        }
        public ActionResult getMeals(int res_id)
        {
            var meals = _context.Meals.Where(x => x.Restaurant_ID == res_id).Select(x => x);
            return View(meals);
        }
        public ActionResult initiateAdditionToOrder(int id, string userName)
        {
            bool flag = _context.Meals.Any(e => e.ID == id);
            if (flag)
            {
                string userID = users.Users.Where(e => e.UserName == userName).Select(e => e.Id).SingleOrDefault();
                int orderID = _context.Orders.Where(e => e.User_ID == userID && e.State == Order.Order_State.not_paid).Select(e => e.ID).SingleOrDefault();
                if (orderID > 0)
                {// jei randam notpaid order
                    _context.OrderedMeals.Add(new OrderedMeal { Amount = 1, Meal_ID = id, Order_ID = orderID });
                }
                else
                {// jei nerandam sukuriam nauja order
                    _context.Orders.Add(new Order { Order_date = DateTime.Now, State = Order.Order_State.not_paid, User_ID=userID});
                    _context.OrderedMeals.Add(new OrderedMeal { Amount = 1, Meal_ID = id, Order_ID = orderID });
                }
                _context.SaveChanges();
                return View("Index", _context.Meals.ToList());
            }
            else return View("Index", showOrderErrorMessage());
        }
        public string showOrderErrorMessage()
        {
            var message = "This meal is not avaliable";
            return message;//View(message);
        }
    }
}

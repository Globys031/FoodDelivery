#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Data;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;

using FoodDelivery.Controllers;

namespace FoodDelivery.Controllers
{
    public class MealsController : Controller
    {
        private readonly MealContext _context;

        public MealsController(MealContext context)
        {
            _context = context;
        }

        // GET: Meals
        public async Task<IActionResult> Index()
        {
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
            return View("/Views/Meals/Index.cshtml", await _context.Meals.ToListAsync());
        }

        private bool AlreadyInOrder(int id)
        {
            return _context.Meals.Any(e => e.ID == id);
        }
    }
}

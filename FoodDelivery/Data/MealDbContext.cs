using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using FoodDelivery.Models;

namespace FoodDelivery.Data
{
    public class MealContext : DbContext
    {
        public MealContext(DbContextOptions<MealContext> options) : base(options)
        {
        }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>().ToTable("Meal");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Restaurant>().ToTable("Restaurant");
        }
    }
}
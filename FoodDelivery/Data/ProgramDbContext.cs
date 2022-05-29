using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using FoodDelivery.Models;

namespace FoodDelivery.Data
{
    public class ProgramContext : DbContext
    {
        public ProgramContext(DbContextOptions<ProgramContext> options) : base(options)
        {
        }

        public DbSet<Meal> Meals { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<OrderedMeal> OrderedMeals { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Meal>().ToTable("Meal");
            modelBuilder.Entity<Order>().ToTable("Order");
            modelBuilder.Entity<Restaurant>().ToTable("Restaurant");
            modelBuilder.Entity<OrderedMeal>().ToTable("Ordered_meal");
            modelBuilder.Entity<Cart>().ToTable("Cart");
            modelBuilder.Entity<Payment>().ToTable("Payment");
        }
    }
}
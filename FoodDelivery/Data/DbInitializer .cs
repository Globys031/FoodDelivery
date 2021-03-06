using FoodDelivery.Models;
using FoodDelivery.Controllers;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ProgramContext context)
        {
            context.Database.EnsureCreated();
            // Look for any meals.
            if (context.Meals.Any())
            {
                return;   // DB has been seeded
            }

            // Lets say there's no order or restaurant with ID=0 (because if not set, it'll default to 0
            var meals = new Meal[]
            {
            new Meal{Restaurant_ID=1,Name="Bulves su kefyru",Price=20.0,Description="Isivaizduok pardavinet bulves su kefyru restorane",Image_file_path=""},
            new Meal{Restaurant_ID=1,Name="Cepelinai",Price=3.99,Description="Cepelinai aprasymas",Image_file_path=""},
            new Meal{Restaurant_ID=1,Name="Grikiai su sviestu",Price=2,Description="aprasymas",Image_file_path=""},
            new Meal{Restaurant_ID=2,Name="Fri bulvytes",Price=4,Description="aprasymas",Image_file_path=""},
            new Meal{Restaurant_ID=2,Name="Sumustiniai su nutella",Price=10.95,Description="Norejau rasyt sumustiniai su kefyru",Image_file_path=""},
            new Meal{Restaurant_ID=2,Name="Plovas",Price=6,Description="Plovas aprasymas",Image_file_path=""},
            new Meal{Restaurant_ID=2,Name="Vistienos slauneles",Price=7,Description="Vistienos slauneles aprasymas",Image_file_path=""}
            };
            foreach (Meal meal in meals)
            {
                context.Meals.Add(meal);
            }
            context.SaveChanges();


            var restaurants = new Restaurant[]
            {
            new Restaurant{Name="Fresh Spot",City="Kaunas",Address="K. Petrausko g. 26", User_ID="d0ece1e8-3e37-4132-ab71-24179ab54ee6"},
            new Restaurant{Name="American Pizza",City="Kaunas",Address="Vytauto pr. 60", User_ID="d0ece1e8-3e37-4132-ab71-24179ab54ee6"},
            };
            foreach (Restaurant restaurant in restaurants)
            {
                context.Restaurants.Add(restaurant);
            }
            context.SaveChanges();
        }

        // Initialize Roles
        public static void InitializeRoles(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any roles.
            if (context.Roles.Any())
            {
                return;   // DB has been seeded
            }

            var roles = new IdentityRole[]
            {
            new IdentityRole{Name="Administrator",NormalizedName="ADMINISTRATOR"},
            new IdentityRole{Name="RestaurantRepresentative",NormalizedName="RESTAURANTREPRESENTATIVE"},
            new IdentityRole{Name="Courier",NormalizedName="COURIER"},
            };
            foreach (IdentityRole role in roles)
            {
                context.Roles.Add(role);
            }
            context.SaveChanges();
        }
        // Initializes users
        public static void InitializeUsers(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any users.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }

            PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>();
            var hashedPassword = passwordHasher.HashPassword(new IdentityUser(), "3456yhdfG`Hw23dfs");

            var users = new IdentityUser[]
            {
            new IdentityUser{UserName = "Joe",NormalizedUserName = "JOE",Email = "wx@hotmail.com",EmailConfirmed = true,PasswordHash = hashedPassword },
            new IdentityUser{UserName="NormalUser", NormalizedUserName="NORMALUSER", Email="NormalUser@email.com", EmailConfirmed=true, PasswordHash=hashedPassword},
            new IdentityUser{UserName="AdministratorUser", NormalizedUserName="ADMINISTRATORUSER", Email="AdministratorUser@email.com", EmailConfirmed=true, PasswordHash=hashedPassword},
            new IdentityUser{UserName="RestaurantManagerUser", NormalizedUserName="RESTAURANTMANAGERUSER", Email="RestaurantManagerUser@email.com", EmailConfirmed=true, PasswordHash=hashedPassword},
            new IdentityUser{UserName="CourierUser", NormalizedUserName="COURIERUSER", Email="CourierUser@email.com", EmailConfirmed=true, PasswordHash=hashedPassword},
            };

            //UserManager<IdentityUser> userManager = new UserManager<IdentityUser>();

            //userManager.AddToRoleAsync(users[2], "Administrator");
            //userManager.AddToRoleAsync(users[3], "RestaurantRepresentative");
            //userManager.AddToRoleAsync(users[4], "Courier");

            foreach (IdentityUser user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}
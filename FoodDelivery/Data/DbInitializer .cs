using FoodDelivery.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodDelivery.Data
{
    public static class DbInitializer
    {
        public static void Initialize(MealContext context)
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
            new Meal{Restaurant_ID=1,Order_ID=1,Name="Bulves su kefyru",Price=20.0,Description="Isivaizduok pardavinet bulves su kefyru restorane",Image_file_path=""},
            new Meal{Restaurant_ID=1,Order_ID=2,Name="Cepelinai",Price=3.99,Description="Cepelinai aprasymas",Image_file_path=""},
            new Meal{Restaurant_ID=1,Order_ID=3,Name="Grikiai su sviestu",Price=2,Description="aprasymas",Image_file_path=""},
            new Meal{Restaurant_ID=2,Order_ID=4,Name="Fri bulvytes",Price=4,Description="aprasymas",Image_file_path=""},
            new Meal{Restaurant_ID=2,Order_ID=4,Name="Sumustiniai su nutella",Price=10.95,Description="Norejau rasyt sumustiniai su kefyru",Image_file_path=""},
            new Meal{Restaurant_ID=2,Order_ID=4,Name="Plovas",Price=6,Description="Plovas aprasymas",Image_file_path=""},
            new Meal{Restaurant_ID=2,Order_ID=4,Name="Vistienos slauneles",Price=7,Description="Vistienos slauneles aprasymas",Image_file_path=""}
            };
            foreach (Meal meal in meals)
            {
                context.Meals.Add(meal);
            }
            context.SaveChanges();


            var restaurants = new Restaurant[]
            {
            new Restaurant{Name="Fresh Spot",City="Kaunas",Address="K. Petrausko g. 26"},
            new Restaurant{Name="American Pizza",City="Kaunas",Address="Vytauto pr. 60"},
            };
            foreach (Restaurant restaurant in restaurants)
            {
                context.Restaurants.Add(restaurant);
            }
            context.SaveChanges();



            var orders = new Order[]
            {
            new Order{Order_date=DateTime.Parse("2005-09-01"),State=Order.Order_State.paid_for},
            new Order{Order_date=DateTime.Parse("2015-10-02"),State=Order.Order_State.paid_for},
            new Order{Order_date=DateTime.Parse("2020-09-11"),State=Order.Order_State.paid_for},
            new Order{Order_date=DateTime.Parse("2022-04-04"),State=Order.Order_State.paid_for}
            };
            foreach (Order order in orders)
            {
                context.Orders.Add(order);
            }
            context.SaveChanges();
        }
        public static void InitializeUsers(ApplicationDbContext context)
        {

            context.Database.EnsureCreated();

            //// Look for any users.
            //if (context.Users.Any())
            //{
            //    return;   // DB has been seeded
            //}


            // Added this user simply so that I would be able to use .HashPassword. Every user will be using the same password for easier access.
            /////////////////////
            /// Didn't work for whatever reason. Check if the database actually added this user properly.
            IdentityUser applicationUser = new IdentityUser();
            Guid guid = Guid.NewGuid();
            applicationUser.Id = guid.ToString();
            applicationUser.UserName = "Joe";
            applicationUser.Email = "wx@hotmail.com";
            applicationUser.NormalizedUserName = "wx@hotmail.com";

            context.Users.Add(applicationUser);


            PasswordHasher<IdentityUser> passwordHasher = new PasswordHasher<IdentityUser>(); 

            var hashedPassword = passwordHasher.HashPassword(applicationUser, "3456yhdfG`Hw23dfs");
            applicationUser.SecurityStamp = Guid.NewGuid().ToString();
            applicationUser.PasswordHash = hashedPassword;

            context.SaveChanges();
            //////////////////////



            // Lets say there's no order or restaurant with ID=0 (because if not set, it'll default to 0
            var users = new IdentityUser[]
            {
            new IdentityUser{UserName="NormalUser", Email="NormalUser@email.com", PasswordHash="3456yhdfG`Hw23dfs"},
            new IdentityUser{UserName="AdministratorUser", Email="NormalUser@email.com", PasswordHash=hashedPassword},
            new IdentityUser{UserName="RestaurantManagerUser", Email="NormalUser@email.com", PasswordHash=hashedPassword},
            new IdentityUser{UserName="CourierUser", Email="NormalUser@email.com", PasswordHash=hashedPassword},
            };
            foreach (IdentityUser user in users)
            {
                context.Users.Add(user);
            }
            context.SaveChanges();
        }
    }
}
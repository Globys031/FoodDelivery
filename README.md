# FoodDelivery

Project uses "ASP.NET Core Web App (Model-View-Controller)" template project with .NET 6.0.
You're gonna have to download visual studio 2022 or download dotnet 6.0 and compile the code from a CLI.

It's possible that after downloading the code (haven't confirmed), you might have to go to Tools -> NuGet Package Manager -> Package Manager Console. Then enter:
Update-Database -Context ApplicationDbContext
Update-Database -Context MealDbContext

To see a database table go to View -> SQL Server Object Explorer.

If you ever need to get rid of current database data, either delete the database (after restarting the project it'll recreate all the files and seeded data), or run:
Drop-Database -Confirm -Context MealContext     (MealContext is just an example)

Most of the code right now was written by following these sources:
https://docs.microsoft.com/en-us/aspnet/core/data/ef-rp/intro?view=aspnetcore-6.0&tabs=visual-studio
https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/working-with-sql?view=aspnetcore-6.0&tabs=visual-studio

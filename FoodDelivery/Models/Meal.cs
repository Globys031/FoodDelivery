namespace FoodDelivery.Models
{
    public class Meal
    {
        public int ID { get; set; }
        public int Restaurant_ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image_file_path { get; set; }

        /*private static string connectionString2 = System.Configuration.ConfigurationManager
            .ConnectionStrings["DefaultConnection"].ConnectionString;
        private readonly MealContext context;*/

        public void redaguoti() { }
        public void checkIfMealExists() { }
        public void addNewMeal() { }
        public void requestData() { }
        public void updateMealData() { }
        public void removeMeal(int ID) { }
        public static bool checkAvailability(int id) {

            bool flag = false;
            /*using (SqlConnection connection = new SqlConnection())
            {

                string query = "SELECT * FROM Meal WHERE Meal.ID = @id";
                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    flag = true;
                }
            }*/
            
            return flag;
        }
        public void returnMeal() { }
    }
}

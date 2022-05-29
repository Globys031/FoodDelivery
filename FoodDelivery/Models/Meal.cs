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
    }
}

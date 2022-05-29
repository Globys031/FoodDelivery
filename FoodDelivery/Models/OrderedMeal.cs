namespace FoodDelivery.Models
{
    public class OrderedMeal
    {
        public int ID { get; set; }
        public int Amount { get; set; }
        public int Meal_ID { get; set; }
        public int Order_ID { get; set; }

        public void createOrderedMeal() { }
    }
}

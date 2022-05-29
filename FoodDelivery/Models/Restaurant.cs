namespace FoodDelivery.Models
{
    public class Restaurant
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string User_ID { get; set; }

        public void checkIfExists() { }
        public void saveRegistrationDetails() { }
        public void getAll() { }
        public void getList() { }
        public void getRestaurant() { }
        public void updateRestaurantData() { }
        public void removeRestaurant() { }
    }
}

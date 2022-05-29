namespace FoodDelivery.Models
{
    public class Cart
    {
        public int ID { get; set; }
        public double Sum { get; set; }
        public int Cond { get; set; }
        public int Rest_ID { get; set; }
        public string User_ID { get; set; }

        public static void createCart()
        {
            return;
        }
    }
}

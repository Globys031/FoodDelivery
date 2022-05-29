namespace FoodDelivery.Models
{
    public class Order
    {
        public enum Order_State
        {
            paid_for,
            declined,
            being_made,
            made,
            delivering,
            delivered
        }

        public int ID { get; set; }
        public DateTime Order_date { get; set; }

        public Order_State State { get; set; }
        public int Cart_ID { get; set; }

        public void pakeistiBusena(Order_State nauja_busena) { }
        public void pridetiPatiekalaPrieUzsakymo() { }
        public void pasalintiPatiekalaIsUzsakymo() { }
        public void checkAvailability() { }
        public void getOrders() { }
        public void updateOrderState() { }
        public void assignCourier() { }
        public void getOrder() { }
    }
}

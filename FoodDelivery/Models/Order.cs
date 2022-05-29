namespace FoodDelivery.Models
{
    public class Order
    {
        public enum Order_State
        {
            not_paid,
            paid_for,
            declined,
            being_made,
            made,
            delivering,
            delivered
        }

        public int ID { get; set; }
        public string User_ID { get; set; }
        public DateTime Order_date { get; set; }

        public Order_State State { get; set; }

        public int? Payment_ID { get; set; }
    }
}

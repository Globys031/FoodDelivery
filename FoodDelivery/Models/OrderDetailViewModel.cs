namespace FoodDelivery.Models
{
    public class OrderDetailViewModel
    {
        public class TMealList
        {
            public string Name { get; set; }
            public int Amount { get; set; }
            public double Price { get; set; }
        }
        public IEnumerable<TMealList> Info { get; set; }
        public double Total { get; set; }
        public Order Order { get; set; }
        public OrderDetailViewModel(IEnumerable<TMealList> info, double total, Order order)
        {
            this.Info = info;
            this.Total = total;
            this.Order = order;
        }
    }
}

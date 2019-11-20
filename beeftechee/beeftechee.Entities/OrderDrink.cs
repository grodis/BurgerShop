namespace beeftechee.Entities
{
    public class OrderDrink
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string Name { get; set; }

        public decimal Litre { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

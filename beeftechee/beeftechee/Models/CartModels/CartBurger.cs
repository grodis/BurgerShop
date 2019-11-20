namespace beeftechee.Models.CartModels
{
    //VIEWMODEL
    public class CartBurger
    {
        public int BurgerId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public string MeatName { get; set; }
        public string BreadName { get; set; }
        public string CheeseName { get; set; }
        public string SauceName { get; set; }
        public string VeggieName { get; set; }


    }
}
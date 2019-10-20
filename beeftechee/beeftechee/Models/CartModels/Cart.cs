using System.Collections.Generic;
using System.Linq;

namespace beeftechee.Models.CartModels
{
    public class Cart
    {
        public List<CartBurger> CartBurgers { get; set; } = new List<CartBurger>();
        public List<CartDrink> CartDrinks { get; set; } = new List<CartDrink>();




        public decimal GetTotalPrice()
        {
            return this.CartBurgers.Sum(x => x.Price * x.Quantity) + this.CartDrinks.Sum(x => x.Price * x.Quantity);
        }
    }
}
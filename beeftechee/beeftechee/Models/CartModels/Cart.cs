using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace beeftechee.Models.CartModels
{
    public class Cart
    {
        public List<CartBurger> CartBurgers { get; set; } = new List<CartBurger>();
        public List<CartDrink> CartDrinks { get; set; } = new List<CartDrink>();
    }
}
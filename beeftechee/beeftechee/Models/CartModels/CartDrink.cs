using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace beeftechee.Models.CartModels
{
    //VIEW MODEL
    public class CartDrink
    {
        public int DrinkId { get; set; }
        public string Name { get; set; }

        public decimal Litre { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
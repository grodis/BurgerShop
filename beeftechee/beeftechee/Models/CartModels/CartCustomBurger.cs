using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace beeftechee.Models.CartModels
{
    //MPOREI KAI NA FYGEI
    public class CartCustomBurger
    {
        public int MeatId { get; set; }
        public int BreadId { get; set; }
        public int? CheeseId { get; set; }
        public int? SauceId { get; set; }
        public int? VeggieId { get; set; }
    }
}
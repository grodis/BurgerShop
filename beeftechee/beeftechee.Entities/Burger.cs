using beeftechee.Entities.Ingredient_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beeftechee.Entities
{
    public class Burger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }



        public int BreadId { get; set; }
        public Bread Bread { get; set; }



        public int MeatId { get; set; }
        public Meat Meat { get; set; }




        public int? CheeseId { get; set; }
        public Cheese Cheese { get; set; }



        public int? SauceId { get; set; }
        public Sauce Sauce { get; set; }



        public int? VeggieId { get; set; }
        public Veggie Veggies { get; set; }


    }
}

using beeftechee.Entities.Ingredient_Entities;
using System.ComponentModel.DataAnnotations;

namespace beeftechee.Entities
{
    public class Burger
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }


        [Display(Name = "Bread")]
        public int BreadId { get; set; }
        public Bread Bread { get; set; }


        [Display(Name = "Meat")]
        public int MeatId { get; set; }
        public Meat Meat { get; set; }



        [Display(Name = "Cheese")]
        public int? CheeseId { get; set; }
        public Cheese Cheese { get; set; }


        [Display(Name = "Sauce")]
        public int? SauceId { get; set; }
        public Sauce Sauce { get; set; }


        [Display(Name = "Veggetable")]
        public int? VeggieId { get; set; }
        public Veggie Veggie { get; set; }

        public string ImageUrl { get; set; }
    }
}

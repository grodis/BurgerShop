using beeftechee.Entities;
using beeftechee.Entities.Ingredient_Entities;
using System.Data.Entity;

namespace beeftechee.Database
{
    public class BeeftecheeDb : DbContext 
    {
        public BeeftecheeDb() : base("name=beeftechee")
        {

        }

        //Burger shop entities database
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderBurger> OrderBurgers { get; set; }
        public DbSet<OrderDrink> OrderDrinks{ get; set; }
        public DbSet<Burger> Burgers { get; set; }
        public DbSet<Drink> Drinks{ get; set; }
        public DbSet<Bread> Breads { get; set; }
        public DbSet<Cheese> Cheeses{ get; set; }
        public DbSet<Meat> Meats{ get; set; }
        public DbSet<Sauce> Sauces{ get; set; }
        public DbSet<Veggie> Veggies{ get; set; }
    }
}

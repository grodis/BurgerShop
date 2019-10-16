using beeftechee.Entities;
using beeftechee.Entities.Ingredient_Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beeftechee.Database
{
    public class BeeftecheeDb : DbContext 
    {
        public BeeftecheeDb() : base("name=beeftechee")
        {

        }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Burger> Burgers { get; set; }
        public DbSet<Drink> Drinks{ get; set; }
        public DbSet<Bread> Breads { get; set; }
        public DbSet<Cheese> Cheeses{ get; set; }
        public DbSet<Meat> Meats{ get; set; }
        public DbSet<Sauce> Sauces{ get; set; }
        public DbSet<Veggie> Veggies{ get; set; }
    }
}

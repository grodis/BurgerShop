using beeftechee.Database;
using beeftechee.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace beeftechee.Services
{
    public class BurgerServices
    {
        //Returns asynchronously all burgers including all the ingredients from the database
        public static async Task<List<Burger>> GetBurgersAsync()
        {
            using (BeeftecheeDb context = new BeeftecheeDb())
            {
                var model = context.Burgers.Include(b => b.Bread).Include(b => b.Cheese).Include(b => b.Meat).Include(b => b.Sauce).Include(b => b.Veggie);

                foreach (var burger in model)
                {
                    //Find and place the objects into the burger by their id
                    burger.Meat = context.Meats.Find(burger.MeatId);
                    burger.Bread = context.Breads.Find(burger.BreadId);
                    burger.Sauce = context.Sauces.Find(burger.SauceId);
                    burger.Veggie = context.Veggies.Find(burger.VeggieId);
                    burger.Cheese = context.Cheeses.Find(burger.CheeseId);

                    //Calculate the Total Price of the burger
                    decimal totalPrice = burger.Bread.Price + burger.Meat.Price;
                    totalPrice += context.Sauces.Find(burger.SauceId) == null ? 0 : context.Sauces.Find(burger.SauceId).Price;
                    totalPrice += context.Cheeses.Find(burger.CheeseId) == null ? 0 : context.Cheeses.Find(burger.CheeseId).Price;
                    totalPrice += context.Veggies.Find(burger.VeggieId) == null ? 0 : context.Veggies.Find(burger.VeggieId).Price;
                    burger.Price = totalPrice;
                }
                return await model.ToListAsync();
            }

        }


        //Returns asynchronously a burger with a specific ID
        public static async Task<Burger> FindBurgerAsync(int? id)
        {
            using (BeeftecheeDb context = new BeeftecheeDb())
            {
                var model = context.Burgers.Include(b => b.Bread).Include(b => b.Cheese).Include(b => b.Meat).Include(b => b.Sauce).Include(b => b.Veggie).SingleOrDefaultAsync(x => x.Id == id);
                return await model;
            }

        }

        public static Burger GetBurger()
        {
            return new Burger();
        }



    }
}

using beeftechee.Database;
using beeftechee.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace beeftechee.Services
{
    public class DrinkServices
    {
        //Returns asynchronously all the drinks from the database
        public static async Task<List<Drink>> GetDrinksAsync()
        {
            using (BeeftecheeDb context = new BeeftecheeDb())
            {
                var model = from x in context.Drinks
                            select x;
                return await  model.ToListAsync();
            }
        }

        //Returns asynchronously a drink with a specific ID
        public static async Task<Drink> FindDrinkAsync(int? id)
        {
            using (BeeftecheeDb context = new BeeftecheeDb())
            {
                var model = context.Drinks.FindAsync(id);
                return await model;
            }

        }

        public static async Task AddDrinkAsync(Drink drink)
        {
            using (BeeftecheeDb context = new BeeftecheeDb())
            {
                context.Drinks.Add(drink);
                await context.SaveChangesAsync();

            }
        }


        public static void UpdateDrink(Drink drink)
        {
            using (BeeftecheeDb context = new BeeftecheeDb())
            {
                context.Entry(drink).State = EntityState.Modified;

            }
        }

        public static async Task SaveChanges()
        {
            using (BeeftecheeDb context = new BeeftecheeDb())
            {
                await context.SaveChangesAsync();

            }
        }

    }
}

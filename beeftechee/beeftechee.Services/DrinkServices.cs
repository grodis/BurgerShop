using beeftechee.Database;
using beeftechee.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beeftechee.Services
{
    public class DrinkServices
    {
        public static async Task<List<Drink>> GetDrinksAsync()
        {
            using (BeeftecheeDb context = new BeeftecheeDb())
            {
                var model = from x in context.Drinks
                            select x;
                return await  model.ToListAsync();
            }
        }

        public static async Task<Drink> FindDrinkAsync(int? id)
        {
            using (BeeftecheeDb context = new BeeftecheeDb())
            {
                var model = context.Drinks.FindAsync(id);
                return await model;
            }

        }


    }
}

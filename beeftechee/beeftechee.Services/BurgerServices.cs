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
    public class BurgerServices
    {
        //Returns asynchronously all burgers including all the ingredients from the database
        public static async Task<List<Burger>> GetBurgersAsync()
        {
            using (BeeftecheeDb context = new BeeftecheeDb())
            {
                var model = context.Burgers.Include(b => b.Bread).Include(b => b.Cheese).Include(b => b.Meat).Include(b => b.Sauce).Include(b => b.Veggie);
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

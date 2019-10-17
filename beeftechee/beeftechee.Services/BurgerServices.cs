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

        public static async Task<List<Burger>> GetBurgersAsync()
        {
            using (BeeftecheeDb context = new BeeftecheeDb())
            {
                var model = from x in context.Burgers
                            select x;
                return await model.ToListAsync();
            }
        }



    }
}

using beeftechee.Database;
using beeftechee.Entities;
using beeftechee.Entities.Ingredient_Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beeftechee.Services
{
    public class VeggieServices
    {
        public static async Task<List<Veggie>> GetVeggiesAsync()
        {
            using (BeeftecheeDb context = new BeeftecheeDb())
            {
                var model = from x in context.Veggies
                            select x;
                return await model.ToListAsync();
            }
        }
    }
}

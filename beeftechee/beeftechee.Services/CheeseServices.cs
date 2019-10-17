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
    public class CheeseServices
    {
        public static async Task<List<Cheese>> GetCheesesAsync()
        {
            using (BeeftecheeDb context = new BeeftecheeDb())
            {
                var model = from x in context.Cheeses
                            select x;
                return await model.ToListAsync();
            }
        }

    }
}

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
    public class BreadServices
    {
        public static async Task<List<Bread>> GetBreadsAsync()
        {
            using (BeeftecheeDb context = new BeeftecheeDb())
            {
                var model = from x in context.Breads
                            select x;
                return await model.ToListAsync();
            }
        }
    }
}

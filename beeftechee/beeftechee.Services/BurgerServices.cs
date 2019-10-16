using beeftechee.Database;
using beeftechee.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beeftechee.Services
{
    public class BurgerServices
    {
        public static List<Burger> GetBurgers()
        {
            using(BeeftecheeDb context = new BeeftecheeDb())
            {
                var model = from x in context.Burgers
                            select x;
                return model.ToList();
            }
        } 
    }
}

using beeftechee.Database;
using beeftechee.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beeftechee.Services
{
    public static class CommonTools
    {
        

        //Returns the max id of the burger database 
        public static int GetBurgerMaxId()
        {
            using(BeeftecheeDb context = new BeeftecheeDb())
            {
                return context.Burgers.Max(y => y.Id);
            }
            
        }
    }
}

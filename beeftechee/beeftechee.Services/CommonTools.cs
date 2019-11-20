using beeftechee.Database;
using System.Linq;

namespace beeftechee.Services
{
    public static class CommonTools
    {
        

        //Returns the max id of the burger database 
        public static int GetBurgerMaxId()
        {
            using(BeeftecheeDb context = new BeeftecheeDb())
            {
                if(context.Burgers.Any())
                    return context.Burgers.Max(y => y.Id);
                return 1;
            }
            
        }
    }
}

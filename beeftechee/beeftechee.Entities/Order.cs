using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beeftechee.Entities
{
    public class Order
    {


        public int Id { get; set; }
        public List<Burger> Burgers { get; set; } = new List<Burger>();
        public List<Drink> Drinks { get; set; } = new List<Drink>();
        public decimal TotalPrice { get; set; }



    }
}

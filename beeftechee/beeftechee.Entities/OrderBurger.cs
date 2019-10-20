using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beeftechee.Entities
{
    public class OrderBurger
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }

        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public string MeatName { get; set; }
        public string BreadName { get; set; }
        public string CheeseName { get; set; }
        public string SauceName { get; set; }
        public string VeggieName { get; set; }
    }
}

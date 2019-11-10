using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace beeftechee.Entities
{
    public class Order
    {


        public int Id { get; set; }
        [DataType(DataType.Currency)]
        public decimal TotalPrice { get; set; }

        public DateTime OrderDate { get; set; }

        public string UserName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public int PostalCode { get; set; }
        public string ContactPhone { get; set; }

        public List<OrderBurger> OrderBurgers { get; set; } = new List<OrderBurger>();
        public List<OrderDrink> OrderDrinks { get; set; } = new List<OrderDrink>();


    }
}

using beeftechee.Entities;
using System.Collections.Generic;

namespace beeftechee.ViewModels
{
    public class BurgerDrinkViewModel
    {
        public List<Burger> Burgers { get; set; } = new List<Burger>();
        public List<Drink> Drinks { get; set; } = new List<Drink>();
    }
}
using beeftechee.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace beeftechee.ViewModels
{
    public class BurgerDrinkViewModel
    {
        public List<Burger> Burgers { get; set; } = new List<Burger>();
        public List<Drink> Drinks { get; set; } = new List<Drink>();
    }
}
using beeftechee.Services;
using beeftechee.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace beeftechee.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }







        public async Task<ActionResult> Menu()
        {
            var model = new BurgerDrinkViewModel
            {
                Burgers = await BurgerServices.GetBurgersAsync(),
                Drinks = await DrinkServices.GetDrinksAsync()
            };

            return  View(model);


        }












        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
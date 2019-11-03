using beeftechee.Database;
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
        private BeeftecheeDb db = new BeeftecheeDb();
        public ActionResult Index()
        {
            return View();
        }







        public async Task<ActionResult> Menu()
        {

            ViewBag.BreadId = new SelectList(db.Breads, "Id", "Name");
            ViewBag.CheeseId = new SelectList(db.Cheeses, "Id", "Name");
            ViewBag.MeatId = new SelectList(db.Meats, "Id", "Name");
            ViewBag.SauceId = new SelectList(db.Sauces, "Id", "Name");
            ViewBag.VeggieId = new SelectList(db.Veggies, "Id", "Name");


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

        [Authorize(Roles = "Admin, Spectator")]
        public ActionResult AdminPanel()
        {
            return View();
        }


    }
}
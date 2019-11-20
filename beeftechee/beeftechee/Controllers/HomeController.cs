using beeftechee.Database;
using beeftechee.Models;
using beeftechee.Services;
using beeftechee.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
        [Authorize]
        public async Task<ActionResult> Chat()
        {
            ApplicationUser user = await System.Web.HttpContext.Current.GetOwinContext()
                                                     .GetUserManager<ApplicationUserManager>()
                                                     .FindByIdAsync(User.Identity.GetUserId());
            return View(user);
        }





        public async Task<ActionResult> Menu()
        {

            ViewBag.BreadId = new SelectList(db.Breads, "Id", "NamePrice");
            ViewBag.CheeseId = new SelectList(db.Cheeses, "Id", "NamePrice");
            ViewBag.MeatId = new SelectList(db.Meats, "Id", "NamePrice");
            ViewBag.SauceId = new SelectList(db.Sauces, "Id", "NamePrice");
            ViewBag.VeggieId = new SelectList(db.Veggies, "Id", "NamePrice");


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

        [Authorize(Roles = "Admin")]
        public ActionResult AdminPanel()
        {
            return View();
        }

        [Authorize(Roles="Supervisor")]
        public ActionResult Demo()
        {
            return View();
        }


    }
}
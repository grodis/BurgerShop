using beeftechee.Database;
using beeftechee.Entities;
using beeftechee.Models.CartModels;
using beeftechee.Services;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace beeftechee.Controllers
{
    public class CartController : Controller
    {
        private readonly BeeftecheeDb db = new BeeftecheeDb();

        // GET: Cart
        public ActionResult Index()
        {
            var cart = CreateOrGetCart();

            return View(cart);
        }



        public async Task<ActionResult> AddToCartBurger(int? BurgerId)
        {
            var burger = await BurgerServices.FindBurgerAsync(BurgerId);






            var cart = CreateOrGetCart();
            var existingBurger = cart.CartBurgers.FirstOrDefault(x => x.BurgerId == burger.Id);

            if (existingBurger != null)
            {
                existingBurger.Quantity++;
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }
            else
            {
                cart.CartBurgers.Add(new CartBurger()
                {
                    BurgerId = burger.Id,
                    Name = burger.Name,
                    Price = burger.Price,
                    Quantity = 1,
                    MeatName = burger.Meat.Name,
                    BreadName = burger.Bread.Name,
                    CheeseName = burger.Cheese.Name,
                    SauceName = burger.Sauce.Name,
                    VeggieName = burger.Veggie.Name
                });
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }

            SaveCart(cart);

            return RedirectToAction("Menu", "Home");
        }








        public ActionResult DeleteBurger(int BurgerId)
        {

            var cart = CreateOrGetCart();

            var existingItem = cart.CartBurgers.FirstOrDefault(x => x.BurgerId == BurgerId);
            var quantityDeleted = existingItem.Quantity;

            if (existingItem != null)
            {
                cart.CartBurgers.Remove(existingItem);
            }
            Session["count"] = Convert.ToInt32(Session["count"]) - quantityDeleted;

            SaveCart(cart);

            return RedirectToAction("Index", "Cart");
        }





        //DRINK
        public async Task<ActionResult> AddToCartDrink(int? DrinkId)
        {
            var drink = await DrinkServices.FindDrinkAsync(DrinkId);

            var cart = CreateOrGetCart();
            var existingDrink = cart.CartDrinks.FirstOrDefault(x => x.DrinkId == drink.Id);

            if (existingDrink != null)
            {
                existingDrink.Quantity++;
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }
            else
            {
                cart.CartDrinks.Add(new CartDrink()
                {
                    DrinkId = drink.Id,
                    Name = drink.Name,
                    Litre = drink.Litre,
                    Price = drink.Price,
                    Quantity = 1
                });
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }

            SaveCart(cart);

            return RedirectToAction("Menu", "Home");
        }



        public async Task<ActionResult> DeleteDrink(int DrinkId)
        {
            var drink = await DrinkServices.FindDrinkAsync(DrinkId);

            var cart = CreateOrGetCart();
            var existingItem = cart.CartDrinks.FirstOrDefault(x => x.DrinkId == drink.Id);
            var quantityDeleted = existingItem.Quantity;

            if (existingItem != null)
            {
                cart.CartDrinks.Remove(existingItem);
            }
            Session["count"] = Convert.ToInt32(Session["count"]) - quantityDeleted;

            SaveCart(cart);

            return RedirectToAction("Index", "Cart");
        }




        public ActionResult AddToCartCustom([Bind(Include = "Name,BreadId,MeatId,CheeseId,SauceId,VeggieId")]Burger burger)
        {
            Session["maxInt"] = Convert.ToInt32(Session["maxInt"]) + 1;
            burger.Id = Convert.ToInt32(Session["maxInt"]);
            if (ModelState.IsValid)
            {
                burger.Name = "Custom Name";
                burger.Meat = db.Meats.Find(burger.MeatId);
                burger.Bread = db.Breads.Find(burger.BreadId);
                burger.Sauce = db.Sauces.Find(burger.SauceId);
                burger.Veggie = db.Veggies.Find(burger.VeggieId);
                burger.Cheese = db.Cheeses.Find(burger.CheeseId);
                decimal totalPrice = burger.Bread.Price + burger.Meat.Price;
                totalPrice += db.Sauces.Find(burger.SauceId) == null ? 0 : db.Sauces.Find(burger.SauceId).Price;
                totalPrice += db.Cheeses.Find(burger.CheeseId) == null ? 0 : db.Cheeses.Find(burger.CheeseId).Price;
                totalPrice += db.Veggies.Find(burger.VeggieId) == null ? 0 : db.Veggies.Find(burger.VeggieId).Price;

                burger.Price = totalPrice;


                var cart = CreateOrGetCart();

                var cartBurger = new CartBurger
                {
                    Price = burger.Price,
                    Quantity = 1,
                    BurgerId = burger.Id,
                    Name = burger.Name,
                    BreadName = burger.Bread.Name,
                    MeatName = burger.Meat.Name
                };

                if (burger.Sauce != null)
                    cartBurger.SauceName = burger.Sauce.Name;

                if (burger.Cheese.Name != null)
                    cartBurger.CheeseName = burger.Cheese.Name;

                if (burger.Veggie.Name != null)
                    cartBurger.VeggieName = burger.Veggie.Name;

                cart.CartBurgers.Add(cartBurger);

                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
                SaveCart(cart);


                return RedirectToAction("Menu", "Home");
            }
            ViewBag.BreadId = new SelectList(db.Breads, "Id", "Name", burger.BreadId);
            ViewBag.CheeseId = new SelectList(db.Cheeses, "Id", "Name", burger.CheeseId);
            ViewBag.MeatId = new SelectList(db.Meats, "Id", "Name", burger.MeatId);
            ViewBag.SauceId = new SelectList(db.Sauces, "Id", "Name", burger.SauceId);
            ViewBag.VeggieId = new SelectList(db.Veggies, "Id", "Name", burger.VeggieId);
            return View(burger);
        }








        private Cart CreateOrGetCart()
        {

            var cart = Session["Cart"] as Cart;
            if (cart == null)
            {
                cart = new Cart();
                SaveCart(cart);
            }

            return cart;
        }

        private void ClearCart()
        {
            var cart = new Cart();
            SaveCart(cart);
        }

        private void SaveCart(Cart cart)
        {
            Session["Cart"] = cart;
        }


        private static int GetMaxId(List<Burger> burgers)
        {
            return burgers.Max(y => y.Id);
        }

    }

}
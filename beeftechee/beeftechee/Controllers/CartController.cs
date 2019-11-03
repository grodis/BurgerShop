using beeftechee.App_Start;
using beeftechee.Database;
using beeftechee.Entities;
using beeftechee.Models;
using beeftechee.Models.CartModels;
using beeftechee.Services;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace beeftechee.Controllers
{
    [Authorize]
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
            //To build later
            if (BurgerId == null)
                return HttpNotFound();


            var burger = await BurgerServices.FindBurgerAsync(BurgerId);

            //Create the cart if it doesnt exist or get the existing cart
            var cart = CreateOrGetCart();

            //Find if the burger exists in the CartBurger list
            var existingBurger = cart.CartBurgers.FirstOrDefault(x => x.BurgerId == burger.Id);

            //If it does,add 1 to its quantity
            if (existingBurger != null)
            {
                existingBurger.Quantity++;
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }
            //If it doesnt,create a new CartBurger and save it into the CartBurgers list of the cart
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

            //Create the cart if it doesnt exist or get the existing cart
            var cart = CreateOrGetCart();

            //Find if the burger exists in the CartBurgers list and find its Quantity
            var existingItem = cart.CartBurgers.FirstOrDefault(x => x.BurgerId == BurgerId);
            var quantityDeleted = existingItem.Quantity;

            //If it does,remove it from the list
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

            //Create the cart if it doesnt exist or get the existing cart
            var cart = CreateOrGetCart();

            //Find if the drink exists in the CartDrink list
            var existingDrink = cart.CartDrinks.FirstOrDefault(x => x.DrinkId == drink.Id);

            //If it does,add 1 to its quantity
            if (existingDrink != null)
            {
                existingDrink.Quantity++;
                Session["count"] = Convert.ToInt32(Session["count"]) + 1;
            }
            //If it doesnt,create a new CartDrink and save it into the CartDrinks list of the cart
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

            //Create the cart if it doesnt exist or get the existing cart
            var cart = CreateOrGetCart();

            //Find if the drink exists in the CartDrinks list and find its Quantity
            var existingItem = cart.CartDrinks.FirstOrDefault(x => x.DrinkId == drink.Id);
            var quantityDeleted = existingItem.Quantity;

            //If it does,remove it from the list
            if (existingItem != null)
            {
                cart.CartDrinks.Remove(existingItem);
            }
            Session["count"] = Convert.ToInt32(Session["count"]) - quantityDeleted;

            SaveCart(cart);

            return RedirectToAction("Index", "Cart");
        }




        public async Task<ActionResult> AddToCartCustom([Bind(Include = "Name,BreadId,MeatId,CheeseId,SauceId,VeggieId")]Burger burger)
        {
            ApplicationUser user = await System.Web.HttpContext.Current.GetOwinContext()
                                                                 .GetUserManager<ApplicationUserManager>()
                                                                 .FindByIdAsync(User.Identity.GetUserId());
            //Set the Sessions maxInt to what it was  +1
            Session["maxInt"] = Convert.ToInt32(Session["maxInt"]) + 1;

            //Set the Sessions maxInt as the customs burger id
            burger.Id = Convert.ToInt32(Session["maxInt"]);
            if (ModelState.IsValid)
            {
                //Find and place the objects into the burger by their id
                burger.Name = $"{user.FirstName}'s Custom Burger";
                burger.Meat = db.Meats.Find(burger.MeatId);
                burger.Bread = db.Breads.Find(burger.BreadId);
                burger.Sauce = db.Sauces.Find(burger.SauceId);
                burger.Veggie = db.Veggies.Find(burger.VeggieId);
                burger.Cheese = db.Cheeses.Find(burger.CheeseId);

                //Calculate the Total Price of the burger
                decimal totalPrice = burger.Bread.Price + burger.Meat.Price;
                totalPrice += db.Sauces.Find(burger.SauceId) == null ? 0 : db.Sauces.Find(burger.SauceId).Price;
                totalPrice += db.Cheeses.Find(burger.CheeseId) == null ? 0 : db.Cheeses.Find(burger.CheeseId).Price;
                totalPrice += db.Veggies.Find(burger.VeggieId) == null ? 0 : db.Veggies.Find(burger.VeggieId).Price;

                burger.Price = totalPrice;


                var cart = CreateOrGetCart();

                //create a new CartBurger and save it into the CartBurgers list of the cart
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

                if (burger.Cheese != null)
                    cartBurger.CheeseName = burger.Cheese.Name;

                if (burger.Veggie != null)
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









        public async Task<ActionResult> Checkout()
        {
            var cart = CreateOrGetCart();
            Entities.Order order = new Entities.Order();
            ApplicationUser user = await System.Web.HttpContext.Current.GetOwinContext()
                                                                 .GetUserManager<ApplicationUserManager>()
                                                                 .FindByIdAsync(User.Identity.GetUserId());
            order.UserName = user.UserName;
            order.Address = user.Address;
            order.PostalCode = user.PostalCode;
            order.City = user.City;
            order.ContactPhone = user.PhoneNumber;
            order.OrderDate = DateTime.Now;
            order.TotalPrice = cart.GetTotalPrice();
            var stringArrayPrice = order.TotalPrice.ToString("#.##").Split(',');
            var StringPriceWithDot = String.Join(".", stringArrayPrice);



            if (cart.CartBurgers.Any() || cart.CartDrinks.Any())
            {
                try
                {
                    var apiContext = PayPalConfiguration.GetAPIContext();
                    string payerId = Request.Params["PayerID"];
                    if (string.IsNullOrEmpty(payerId))
                    {
                        var itemList = new ItemList()
                        {
                            items = new List<Item>()
                    {
                        new Item()
                        {
                            name = order.UserName + "'s Order",
                            currency = "USD",
                            price = StringPriceWithDot,
                            quantity = "1",
                            sku = "Burgers&Drinks"
                        }
                    }
                        };

                        var payer = new Payer() { payment_method = "paypal" };

                        var baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Cart/Checkout?";
                        var guid = Convert.ToString((new Random()).Next(100000));
                        var redirectUrl = baseURI + "guid=" + guid;
                        var redirUrls = new RedirectUrls()
                        {
                            cancel_url = redirectUrl + "&cancel=true",
                            return_url = redirectUrl
                        };

                        var details = new Details()
                        {
                            subtotal = StringPriceWithDot
                        };

                        var amount = new Amount()
                        {
                            currency = "USD",
                            total = StringPriceWithDot, // Total must be equal to sum of shipping, tax and subtotal.
                            details = details
                        };

                        var transactionList = new List<Transaction>();
                        transactionList.Add(new Transaction()
                        {
                            description = "Transaction description.",
                            invoice_number = CommonPayPal.GetRandomInvoiceNumber(),
                            amount = amount,
                            item_list = itemList,
                            payee = new Payee
                            {
                                email = "BurgerShop@example.com"
                            }
                        });

                        var payment = new Payment()
                        {
                            intent = "sale",
                            payer = payer,
                            transactions = transactionList,
                            redirect_urls = redirUrls
                        };
                        var createdPayment = payment.Create(apiContext);
                        var links = createdPayment.links.GetEnumerator();

                        string paypalRedirectUrl = null;

                        while (links.MoveNext())
                        {
                            Links lnk = links.Current;

                            if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                            {
                                //saving the payapalredirect URL to which user will be redirected for payment
                                paypalRedirectUrl = lnk.href;
                            }
                        }

                        // saving the paymentID in the key guid
                        Session.Add(guid, createdPayment.id);

                        return Redirect(paypalRedirectUrl);
                    }
                    else
                    {

                        // This function exectues after receving all parameters for the payment

                        var guid = Request.Params["guid"];
                        var paymentId = Session[guid] as string;
                        var paymentExecution = new PaymentExecution() { payer_id = payerId };
                        var payment = new Payment() { id = paymentId };

                        var executedPayment = payment.Execute(apiContext, paymentExecution);

                        //If executed payment failed then we will show payment failure message to user
                        if (executedPayment.state.ToLower() != "approved")
                        {
                            return View("FailureView");
                        }
                    }
                }
                catch (PayPal.PaymentsException ex)
                {
                    Debug.WriteLine(ex.InnerException);
                    Debug.WriteLine("----------------------");
                    Debug.WriteLine(ex.Data);
                    Debug.WriteLine("----------------------");
                    Debug.WriteLine(ex.Details);
                    Debug.WriteLine("----------------------");

                    Debug.WriteLine(ex.Response);
                    

                }
                catch (Exception ex)
                {
                    return View("FailureView");
                }
                if (cart.CartBurgers.Any())
                {
                    foreach (var burger in cart.CartBurgers)
                    {
                        order.OrderBurgers.Add(new OrderBurger
                        {
                            Name = burger.Name,
                            Price = burger.Price,
                            Quantity = burger.Quantity,
                            MeatName = burger.MeatName,
                            BreadName = burger.BreadName,
                            CheeseName = burger.CheeseName,
                            SauceName = burger.SauceName,
                            VeggieName = burger.VeggieName
                        });
                    }
                }

                if (cart.CartDrinks.Any())
                {
                    foreach (var drink in cart.CartDrinks)
                    {
                        order.OrderDrinks.Add(new OrderDrink
                        {
                            Name = drink.Name,
                            Litre = drink.Litre,
                            Quantity = drink.Quantity,
                            Price = drink.Price
                        });
                    }
                }

                db.Orders.Add(order);
                await db.SaveChangesAsync();

                //on successful payment, show success page to user.
                Session["count"] = 0;
                ClearCart();
                return View("SuccessView", order);
            }


            return RedirectToAction("Index", "Cart");
        }





        //Check if there is a cart.If there isnt create a new one,otherwise return the existing one.
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

        //Clear the cart
        private void ClearCart()
        {
            var cart = new Cart();
            SaveCart(cart);
        }

        //Save to the cart session
        private void SaveCart(Cart cart)
        {
            Session["Cart"] = cart;
        }


    }

}
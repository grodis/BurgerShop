﻿using beeftechee.Database;
using beeftechee.Entities;
using beeftechee.Services;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace beeftechee.Controllers
{
    [Authorize(Roles = "Admin")]

    public class BurgerController : Controller
    {
        private readonly BeeftecheeDb db = new BeeftecheeDb();

        // GET: Burger
        public async Task<ActionResult> Index()
        {
            return View(await BurgerServices.GetBurgersAsync());
        }


        // GET: Burger/Create
        public ActionResult Create()
        {
            ViewBag.BreadId = new SelectList(db.Breads, "Id", "Name");
            ViewBag.CheeseId = new SelectList(db.Cheeses, "Id", "Name");
            ViewBag.MeatId = new SelectList(db.Meats, "Id", "Name");
            ViewBag.SauceId = new SelectList(db.Sauces, "Id", "Name");
            ViewBag.VeggieId = new SelectList(db.Veggies, "Id", "Name");
            return View();
        }

        // POST: Burger/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,BreadId,MeatId,CheeseId,SauceId,VeggieId")] Burger burger, HttpPostedFileBase ImageUrl)
        {
            if (ModelState.IsValid)
            {
                //Find and place the objects into the burger by their id
                burger.Meat = db.Meats.Find(burger.MeatId);
                burger.Bread = db.Breads.Find(burger.BreadId);
                burger.Sauce = db.Sauces.Find(burger.SauceId);
                burger.Veggie = db.Veggies.Find(burger.VeggieId);
                burger.Cheese = db.Cheeses.Find(burger.CheeseId);

                //Calculate the Total Price of the burger

                burger.Price = GetTotalPrice(burger);


                //Get the photo
                if (ImageUrl != null)
                {
                    ImageUrl.SaveAs(Server.MapPath("~/Content/BurgerImages/" + ImageUrl.FileName));
                    burger.ImageUrl = ImageUrl.FileName;
                }



                //Save the Burger into the database
                db.Burgers.Add(burger);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BreadId = new SelectList(db.Breads, "Id", "Name", burger.BreadId);
            ViewBag.CheeseId = new SelectList(db.Cheeses, "Id", "Name", burger.CheeseId);
            ViewBag.MeatId = new SelectList(db.Meats, "Id", "Name", burger.MeatId);
            ViewBag.SauceId = new SelectList(db.Sauces, "Id", "Name", burger.SauceId);
            ViewBag.VeggieId = new SelectList(db.Veggies, "Id", "Name", burger.VeggieId);
            return View(burger);
        }

        // GET: Burger/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                // return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                return View("Error");
            }
            Burger burger = await db.Burgers.FindAsync(id);
            if (burger == null)
            {
                return View("Error");
            }
            ViewBag.BreadId = new SelectList(db.Breads, "Id", "Name", burger.BreadId);
            ViewBag.CheeseId = new SelectList(db.Cheeses, "Id", "Name", burger.CheeseId);
            ViewBag.MeatId = new SelectList(db.Meats, "Id", "Name", burger.MeatId);
            ViewBag.SauceId = new SelectList(db.Sauces, "Id", "Name", burger.SauceId);
            ViewBag.VeggieId = new SelectList(db.Veggies, "Id", "Name", burger.VeggieId);
            return View(burger);
        }

        // POST: Burger/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,BreadId,MeatId,CheeseId,SauceId,VeggieId")] Burger burger, HttpPostedFileBase ImageUrl)
        {
            if (ModelState.IsValid)
            {
                
                if (ImageUrl != null)
                {
                    ImageUrl.SaveAs(Server.MapPath("~/Content/BurgerImages/" + ImageUrl.FileName));
                    burger.ImageUrl = ImageUrl.FileName;
                }

                //Find and place the objects into the burger by their id
                burger.Meat = db.Meats.Find(burger.MeatId);
                burger.Bread = db.Breads.Find(burger.BreadId);
                burger.Sauce = db.Sauces.Find(burger.SauceId);
                burger.Veggie = db.Veggies.Find(burger.VeggieId);
                burger.Cheese = db.Cheeses.Find(burger.CheeseId);

                //Calculate the Total Price of the burger
                
                burger.Price = GetTotalPrice(burger);

                db.Entry(burger).State = EntityState.Modified;

                //Dont modify property Image

                if (ImageUrl == null)
                    db.Entry(burger).Property(m => m.ImageUrl).IsModified = false;


                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BreadId = new SelectList(db.Breads, "Id", "Name", burger.BreadId);
            ViewBag.CheeseId = new SelectList(db.Cheeses, "Id", "Name", burger.CheeseId);
            ViewBag.MeatId = new SelectList(db.Meats, "Id", "Name", burger.MeatId);
            ViewBag.SauceId = new SelectList(db.Sauces, "Id", "Name", burger.SauceId);
            ViewBag.VeggieId = new SelectList(db.Veggies, "Id", "Name", burger.VeggieId);
            return View(burger);
        }

        // GET: Burger/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Burger burger = await BurgerServices.FindBurgerAsync(id);
            if (burger == null)
            {
                return View("Error");
            }
            return View(burger);
        }

        // POST: Burger/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Burger burger = await db.Burgers.FindAsync(id);
            db.Burgers.Remove(burger);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [NonAction]
        protected decimal GetTotalPrice(Burger burger)
        {
            decimal totalPrice = burger.Bread.Price + burger.Meat.Price;
            totalPrice += db.Sauces.Find(burger.SauceId) == null ? 0 : db.Sauces.Find(burger.SauceId).Price;
            totalPrice += db.Cheeses.Find(burger.CheeseId) == null ? 0 : db.Cheeses.Find(burger.CheeseId).Price;
            totalPrice += db.Veggies.Find(burger.VeggieId) == null ? 0 : db.Veggies.Find(burger.VeggieId).Price;
            return totalPrice;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

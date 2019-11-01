using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using beeftechee.Database;
using beeftechee.Entities;

namespace beeftechee.Controllers
{
    public class DrinkController : Controller
    {
        private BeeftecheeDb db = new BeeftecheeDb();

        // GET: Drink
        public async Task<ActionResult> Index()
        {
            return View(await db.Drinks.ToListAsync());
        }

        // GET: Drink/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drink drink = await db.Drinks.FindAsync(id);
            if (drink == null)
            {
                return HttpNotFound();
            }
            return View(drink);
        }

        // GET: Drink/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Drink/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Litre,Price")] Drink drink, HttpPostedFileBase ImageUrl)
        {
            if (ModelState.IsValid)
            {
                //Get the photo
                if (ImageUrl != null)
                {
                    ImageUrl.SaveAs(Server.MapPath("~/Content/DrinkImages/" + ImageUrl.FileName));
                    drink.ImageUrl = ImageUrl.FileName;
                }


                db.Drinks.Add(drink);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(drink);
        }

        // GET: Drink/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drink drink = await db.Drinks.FindAsync(id);
            if (drink == null)
            {
                return HttpNotFound();
            }
            return View(drink);
        }

        // POST: Drink/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Litre,Price")] Drink drink, HttpPostedFileBase ImageUrl)
        {
            if (ModelState.IsValid)
            {
                //Get the photo
                if (ImageUrl != null)
                {
                    ImageUrl.SaveAs(Server.MapPath("~/Content/DrinkImages/" + ImageUrl.FileName));
                    drink.ImageUrl = ImageUrl.FileName;
                }

                db.Entry(drink).State = EntityState.Modified;

                if (ImageUrl == null)
                    db.Entry(drink).Property(m => m.ImageUrl).IsModified = false;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(drink);
        }

        // GET: Drink/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Drink drink = await db.Drinks.FindAsync(id);
            if (drink == null)
            {
                return HttpNotFound();
            }
            return View(drink);
        }

        // POST: Drink/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Drink drink = await db.Drinks.FindAsync(id);
            db.Drinks.Remove(drink);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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

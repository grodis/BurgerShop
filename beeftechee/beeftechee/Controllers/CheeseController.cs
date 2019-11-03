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
using beeftechee.Entities.Ingredient_Entities;

namespace beeftechee.Controllers
{
    public class CheeseController : Controller
    {
        private BeeftecheeDb db = new BeeftecheeDb();

        // GET: Cheese
        public async Task<ActionResult> Index()
        {
            return View(await db.Cheeses.ToListAsync());
        }

       
        // GET: Cheese/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cheese/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Price")] Cheese cheese)
        {
            if (ModelState.IsValid)
            {
                db.Cheeses.Add(cheese);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(cheese);
        }

        // GET: Cheese/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cheese cheese = await db.Cheeses.FindAsync(id);
            if (cheese == null)
            {
                return HttpNotFound();
            }
            return View(cheese);
        }

        // POST: Cheese/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Price")] Cheese cheese)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cheese).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(cheese);
        }

        // GET: Cheese/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cheese cheese = await db.Cheeses.FindAsync(id);
            if (cheese == null)
            {
                return HttpNotFound();
            }
            return View(cheese);
        }

        // POST: Cheese/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cheese cheese = await db.Cheeses.FindAsync(id);
            db.Cheeses.Remove(cheese);
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

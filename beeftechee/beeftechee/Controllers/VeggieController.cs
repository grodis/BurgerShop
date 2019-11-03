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
    public class VeggieController : Controller
    {
        private BeeftecheeDb db = new BeeftecheeDb();

        // GET: Veggie
        public async Task<ActionResult> Index()
        {
            return View(await db.Veggies.ToListAsync());
        }

        

        // GET: Veggie/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Veggie/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Price")] Veggie veggie)
        {
            if (ModelState.IsValid)
            {
                db.Veggies.Add(veggie);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(veggie);
        }

        // GET: Veggie/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veggie veggie = await db.Veggies.FindAsync(id);
            if (veggie == null)
            {
                return HttpNotFound();
            }
            return View(veggie);
        }

        // POST: Veggie/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Price")] Veggie veggie)
        {
            if (ModelState.IsValid)
            {
                db.Entry(veggie).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(veggie);
        }

        // GET: Veggie/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Veggie veggie = await db.Veggies.FindAsync(id);
            if (veggie == null)
            {
                return HttpNotFound();
            }
            return View(veggie);
        }

        // POST: Veggie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Veggie veggie = await db.Veggies.FindAsync(id);
            db.Veggies.Remove(veggie);
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

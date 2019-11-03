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
    public class BreadController : Controller
    {
        private BeeftecheeDb db = new BeeftecheeDb();

        // GET: Bread
        public async Task<ActionResult> Index()
        {
            return View(await db.Breads.ToListAsync());
        }

        
        // GET: Bread/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bread/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Price")] Bread bread)
        {
            if (ModelState.IsValid)
            {
                db.Breads.Add(bread);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(bread);
        }

        // GET: Bread/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bread bread = await db.Breads.FindAsync(id);
            if (bread == null)
            {
                return HttpNotFound();
            }
            return View(bread);
        }

        // POST: Bread/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Price")] Bread bread)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bread).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bread);
        }

        // GET: Bread/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bread bread = await db.Breads.FindAsync(id);
            if (bread == null)
            {
                return HttpNotFound();
            }
            return View(bread);
        }

        // POST: Bread/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Bread bread = await db.Breads.FindAsync(id);
            db.Breads.Remove(bread);
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

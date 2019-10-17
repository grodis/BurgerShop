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
    public class MeatController : Controller
    {
        private BeeftecheeDb db = new BeeftecheeDb();

        // GET: Meat
        public async Task<ActionResult> Index()
        {
            return View(await db.Meats.ToListAsync());
        }

        // GET: Meat/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meat meat = await db.Meats.FindAsync(id);
            if (meat == null)
            {
                return HttpNotFound();
            }
            return View(meat);
        }

        // GET: Meat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Meat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Price")] Meat meat)
        {
            if (ModelState.IsValid)
            {
                db.Meats.Add(meat);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(meat);
        }

        // GET: Meat/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meat meat = await db.Meats.FindAsync(id);
            if (meat == null)
            {
                return HttpNotFound();
            }
            return View(meat);
        }

        // POST: Meat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Price")] Meat meat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(meat).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(meat);
        }

        // GET: Meat/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Meat meat = await db.Meats.FindAsync(id);
            if (meat == null)
            {
                return HttpNotFound();
            }
            return View(meat);
        }

        // POST: Meat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Meat meat = await db.Meats.FindAsync(id);
            db.Meats.Remove(meat);
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

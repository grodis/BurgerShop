using beeftechee.Database;
using beeftechee.Entities.Ingredient_Entities;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace beeftechee.Controllers
{
    [Authorize(Roles = "Admin")]

    public class SauceController : Controller
    {
        private BeeftecheeDb db = new BeeftecheeDb();

        // GET: Sauce
        public async Task<ActionResult> Index()
        {
            return View(await db.Sauces.ToListAsync());
        }

        

        // GET: Sauce/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sauce/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Price")] Sauce sauce)
        {
            if (ModelState.IsValid)
            {
                db.Sauces.Add(sauce);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(sauce);
        }

        // GET: Sauce/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Sauce sauce = await db.Sauces.FindAsync(id);
            if (sauce == null)
            {
                return View("Error");
            }
            return View(sauce);
        }

        // POST: Sauce/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Price")] Sauce sauce)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sauce).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(sauce);
        }

        // GET: Sauce/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Sauce sauce = await db.Sauces.FindAsync(id);
            if (sauce == null)
            {
                return View("Error");
            }
            return View(sauce);
        }

        // POST: Sauce/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Sauce sauce = await db.Sauces.FindAsync(id);
            db.Sauces.Remove(sauce);
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

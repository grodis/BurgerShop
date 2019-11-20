using beeftechee.Database;
using beeftechee.Entities;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace beeftechee.Controllers
{
    [Authorize(Roles = "Admin")]

    public class OrderController : Controller
    {
        private readonly BeeftecheeDb db = new BeeftecheeDb();

        // GET: Order
        public async Task<ActionResult> Index()
        {
            return View(await db.Orders.Include(x => x.OrderBurgers).Include(x=> x.OrderDrinks).ToListAsync());
        }


        // GET: Order/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Order order = await db.Orders.FindAsync(id);
            if (order == null)
            {
                return View("Error");
            }
            return View(order);
        }

        // POST: Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Order order = await db.Orders.FindAsync(id);
            db.Orders.Remove(order);
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

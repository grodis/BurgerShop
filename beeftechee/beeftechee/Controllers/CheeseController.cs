using beeftechee.Entities.Ingredient_Entities;
using beeftechee.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace beeftechee.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CheeseController : Controller
    {
        private IngredientServices IngredientServices = new IngredientServices();
        // GET: Cheese
        public async Task<ActionResult> Index()
        {
            return View(await IngredientServices.GetAllAsync<Cheese>());
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
                await IngredientServices.AddAsync(cheese);
                return RedirectToAction("Index");
            }

            return View(cheese);
        }

        // GET: Cheese/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Cheese cheese = await IngredientServices.GetAsync<Cheese>(id);
            if (cheese == null)
            {
                return View("Error");
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
                await IngredientServices.UpdateAsync(cheese);
                return RedirectToAction("Index");
            }
            return View(cheese);
        }

        // GET: Cheese/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Cheese cheese = await IngredientServices.GetAsync<Cheese>(id);
            if (cheese == null)
            {
                return View("Error");
            }
            return View(cheese);
        }

        // POST: Cheese/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cheese cheese = await IngredientServices.GetAsync<Cheese>(id);
            await IngredientServices.DeleteAsync(cheese);
            return RedirectToAction("Index");
        }
    }
}

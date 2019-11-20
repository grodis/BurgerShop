using beeftechee.Entities.Ingredient_Entities;
using beeftechee.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace beeftechee.Controllers
{
    [Authorize(Roles = "Admin")]

    public class MeatController : Controller
    {
        private IngredientServices IngredientServices = new Services.IngredientServices();

        // GET: Meat
        public async Task<ActionResult> Index()
        {
            return View(await IngredientServices.GetAllAsync<Meat>());
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
                await IngredientServices.AddAsync(meat);
                return RedirectToAction("Index");
            }

            return View(meat);
        }

        // GET: Meat/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Meat meat = await IngredientServices.GetAsync<Meat>(id);
            if (meat == null)
            {
                return View("Error");
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
                await IngredientServices.UpdateAsync(meat);
                return RedirectToAction("Index");
            }
            return View(meat);
        }

        // GET: Meat/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Meat meat = await IngredientServices.GetAsync<Meat>(id);
            if (meat == null)
            {
                return View("Error");
            }
            return View(meat);
        }

        // POST: Meat/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Meat meat = await IngredientServices.GetAsync<Meat>(id);
            await IngredientServices.DeleteAsync(meat);
            return RedirectToAction("Index");
        }
    }
}

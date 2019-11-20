using beeftechee.Entities.Ingredient_Entities;
using beeftechee.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace beeftechee.Controllers
{
    [Authorize(Roles ="Admin")]
    public class BreadController : Controller
    {
        private IngredientServices IngredientServices = new Services.IngredientServices();

        // GET: Bread
        public async Task<ActionResult> Index()
        {
            return View(await IngredientServices.GetAllAsync<Bread>());
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
                await IngredientServices.AddAsync(bread);
                return RedirectToAction("Index");
            }

            return View(bread);
        }

        // GET: Bread/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Bread bread = await IngredientServices.GetAsync<Bread>(id);
            if (bread == null)
            {
                return View("Error");
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
                await IngredientServices.UpdateAsync(bread);
                return RedirectToAction("Index");
            }
            return View(bread);
        }

        // GET: Bread/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Bread bread = await IngredientServices.GetAsync<Bread>(id);
            if (bread == null)
            {
                return View("Error");
            }
            return View(bread);
        }

        // POST: Bread/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Bread bread = await IngredientServices.GetAsync<Bread>(id);
            await IngredientServices.DeleteAsync(bread);
            return RedirectToAction("Index");
        }
    }
}

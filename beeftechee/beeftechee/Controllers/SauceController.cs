using beeftechee.Entities.Ingredient_Entities;
using beeftechee.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace beeftechee.Controllers
{
    [Authorize(Roles = "Admin")]

    public class SauceController : Controller
    {
        private IngredientServices IngredientServices = new Services.IngredientServices();

        // GET: Sauce
        public async Task<ActionResult> Index()
        {
            return View(await IngredientServices.GetAllAsync<Sauce>());
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
                await IngredientServices.AddAsync(sauce);
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
            Sauce sauce = await IngredientServices.GetAsync<Sauce>(id);
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
                await IngredientServices.UpdateAsync(sauce);
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
            Sauce sauce = await IngredientServices.GetAsync<Sauce>(id);
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
            Sauce sauce = await IngredientServices.GetAsync<Sauce>(id);
            await IngredientServices.DeleteAsync(sauce);
            return RedirectToAction("Index");
        }
    }
}

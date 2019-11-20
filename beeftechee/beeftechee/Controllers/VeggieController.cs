using beeftechee.Entities.Ingredient_Entities;
using beeftechee.Services;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace beeftechee.Controllers
{
    [Authorize(Roles = "Admin")]

    public class VeggieController : Controller
    {
        private IngredientServices IngredientServices = new Services.IngredientServices();

        // GET: Veggie
        public async Task<ActionResult> Index()
        {
            return View(await IngredientServices.GetAllAsync<Veggie>());
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
                await IngredientServices.AddAsync(veggie);
                return RedirectToAction("Index");
            }

            return View(veggie);
        }

        // GET: Veggie/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Veggie veggie = await IngredientServices.GetAsync<Veggie>(id);
            if (veggie == null)
            {
                return View("Error");
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
                await IngredientServices.UpdateAsync(veggie);
                return RedirectToAction("Index");
            }
            return View(veggie);
        }

        // GET: Veggie/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Veggie veggie = await IngredientServices.GetAsync<Veggie>(id);
            if (veggie == null)
            {
                return View("Error");
            }
            return View(veggie);
        }

        // POST: Veggie/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Veggie veggie = await IngredientServices.GetAsync<Veggie>(id);
            await IngredientServices.DeleteAsync(veggie);
            return RedirectToAction("Index");
        }
    }
}

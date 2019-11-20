using beeftechee.Database;
using beeftechee.Entities;
using beeftechee.Services;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace beeftechee.REST_Controllers
{
    public class BurgerApiController : ApiController
    {
        private BeeftecheeDb db = new BeeftecheeDb();

        // GET: api/BurgerApi
        public async Task<List<Burger>> GetBurgers()
        {
            return await BurgerServices.GetBurgersAsync();
        }

        // GET: api/BurgerApi/5
        [ResponseType(typeof(Burger))]
        public async Task<IHttpActionResult> GetBurger(int? id)
        {
            Burger burger = await BurgerServices.FindBurgerAsync(id);
            if (burger == null)
            {
                return NotFound();
            }

            return Ok(burger);
        }

        // PUT: api/BurgerApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBurger(int id, Burger burger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != burger.Id)
            {
                return BadRequest();
            }

            db.Entry(burger).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BurgerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/BurgerApi
        [ResponseType(typeof(Burger))]
        public async Task<IHttpActionResult> PostBurger(Burger burger)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Burgers.Add(burger);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = burger.Id }, burger);
        }

        // DELETE: api/BurgerApi/5
        [ResponseType(typeof(Burger))]
        public async Task<IHttpActionResult> DeleteBurger(int id)
        {
            Burger burger = await db.Burgers.FindAsync(id);
            if (burger == null)
            {
                return NotFound();
            }

            db.Burgers.Remove(burger);
            await db.SaveChangesAsync();

            return Ok(burger);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BurgerExists(int id)
        {
            return db.Burgers.Count(e => e.Id == id) > 0;
        }
    }
}
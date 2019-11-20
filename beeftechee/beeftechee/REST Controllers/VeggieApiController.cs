using beeftechee.Database;
using beeftechee.Entities.Ingredient_Entities;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace beeftechee.REST_Controllers
{
    public class VeggieApiController : ApiController
    {
        private BeeftecheeDb db = new BeeftecheeDb();

        // GET: api/VeggieApi
        public IQueryable<Veggie> GetVeggies()
        {
            return db.Veggies;
        }

        // GET: api/VeggieApi/5
        [ResponseType(typeof(Veggie))]
        public async Task<IHttpActionResult> GetVeggie(int id)
        {
            Veggie veggie = await db.Veggies.FindAsync(id);
            if (veggie == null)
            {
                return NotFound();
            }

            return Ok(veggie);
        }

        // PUT: api/VeggieApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutVeggie(int id, Veggie veggie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != veggie.Id)
            {
                return BadRequest();
            }

            db.Entry(veggie).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VeggieExists(id))
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

        // POST: api/VeggieApi
        [ResponseType(typeof(Veggie))]
        public async Task<IHttpActionResult> PostVeggie(Veggie veggie)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Veggies.Add(veggie);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = veggie.Id }, veggie);
        }

        // DELETE: api/VeggieApi/5
        [ResponseType(typeof(Veggie))]
        public async Task<IHttpActionResult> DeleteVeggie(int id)
        {
            Veggie veggie = await db.Veggies.FindAsync(id);
            if (veggie == null)
            {
                return NotFound();
            }

            db.Veggies.Remove(veggie);
            await db.SaveChangesAsync();

            return Ok(veggie);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VeggieExists(int id)
        {
            return db.Veggies.Count(e => e.Id == id) > 0;
        }
    }
}
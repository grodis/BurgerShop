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
    public class SauceApiController : ApiController
    {
        private BeeftecheeDb db = new BeeftecheeDb();

        // GET: api/SauceApi
        public IQueryable<Sauce> GetSauces()
        {
            return db.Sauces;
        }

        // GET: api/SauceApi/5
        [ResponseType(typeof(Sauce))]
        public async Task<IHttpActionResult> GetSauce(int id)
        {
            Sauce sauce = await db.Sauces.FindAsync(id);
            if (sauce == null)
            {
                return NotFound();
            }

            return Ok(sauce);
        }

        // PUT: api/SauceApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSauce(int id, Sauce sauce)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sauce.Id)
            {
                return BadRequest();
            }

            db.Entry(sauce).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SauceExists(id))
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

        // POST: api/SauceApi
        [ResponseType(typeof(Sauce))]
        public async Task<IHttpActionResult> PostSauce(Sauce sauce)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sauces.Add(sauce);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sauce.Id }, sauce);
        }

        // DELETE: api/SauceApi/5
        [ResponseType(typeof(Sauce))]
        public async Task<IHttpActionResult> DeleteSauce(int id)
        {
            Sauce sauce = await db.Sauces.FindAsync(id);
            if (sauce == null)
            {
                return NotFound();
            }

            db.Sauces.Remove(sauce);
            await db.SaveChangesAsync();

            return Ok(sauce);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SauceExists(int id)
        {
            return db.Sauces.Count(e => e.Id == id) > 0;
        }
    }
}
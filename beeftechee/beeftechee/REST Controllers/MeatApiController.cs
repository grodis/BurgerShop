using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using beeftechee.Database;
using beeftechee.Entities.Ingredient_Entities;

namespace beeftechee.REST_Controllers
{
    public class MeatApiController : ApiController
    {
        private BeeftecheeDb db = new BeeftecheeDb();

        // GET: api/MeatApi
        public IQueryable<Meat> GetMeats()
        {
            return db.Meats;
        }

        // GET: api/MeatApi/5
        [ResponseType(typeof(Meat))]
        public async Task<IHttpActionResult> GetMeat(int id)
        {
            Meat meat = await db.Meats.FindAsync(id);
            if (meat == null)
            {
                return NotFound();
            }

            return Ok(meat);
        }

        // PUT: api/MeatApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMeat(int id, Meat meat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != meat.Id)
            {
                return BadRequest();
            }

            db.Entry(meat).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeatExists(id))
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

        // POST: api/MeatApi
        [ResponseType(typeof(Meat))]
        public async Task<IHttpActionResult> PostMeat(Meat meat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Meats.Add(meat);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = meat.Id }, meat);
        }

        // DELETE: api/MeatApi/5
        [ResponseType(typeof(Meat))]
        public async Task<IHttpActionResult> DeleteMeat(int id)
        {
            Meat meat = await db.Meats.FindAsync(id);
            if (meat == null)
            {
                return NotFound();
            }

            db.Meats.Remove(meat);
            await db.SaveChangesAsync();

            return Ok(meat);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MeatExists(int id)
        {
            return db.Meats.Count(e => e.Id == id) > 0;
        }
    }
}
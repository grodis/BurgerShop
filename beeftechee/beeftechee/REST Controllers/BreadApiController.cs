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
    public class BreadApiController : ApiController
    {
        private BeeftecheeDb db = new BeeftecheeDb();

        // GET: api/BreadApi
        public IQueryable<Bread> GetBreads()
        {
            return db.Breads;
        }

        // GET: api/BreadApi/5
        [ResponseType(typeof(Bread))]
        public async Task<IHttpActionResult> GetBread(int id)
        {
            Bread bread = await db.Breads.FindAsync(id);
            if (bread == null)
            {
                return NotFound();
            }

            return Ok(bread);
        }

        // PUT: api/BreadApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBread(int id, Bread bread)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bread.Id)
            {
                return BadRequest();
            }

            db.Entry(bread).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreadExists(id))
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

        // POST: api/BreadApi
        [ResponseType(typeof(Bread))]
        public async Task<IHttpActionResult> PostBread(Bread bread)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Breads.Add(bread);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = bread.Id }, bread);
        }

        // DELETE: api/BreadApi/5
        [ResponseType(typeof(Bread))]
        public async Task<IHttpActionResult> DeleteBread(int id)
        {
            Bread bread = await db.Breads.FindAsync(id);
            if (bread == null)
            {
                return NotFound();
            }

            db.Breads.Remove(bread);
            await db.SaveChangesAsync();

            return Ok(bread);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BreadExists(int id)
        {
            return db.Breads.Count(e => e.Id == id) > 0;
        }
    }
}
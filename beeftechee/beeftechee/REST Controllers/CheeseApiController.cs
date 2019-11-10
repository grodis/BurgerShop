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
    public class CheeseApiController : ApiController
    {
        private BeeftecheeDb db = new BeeftecheeDb();

        // GET: api/CheeseApi
        public IQueryable<Cheese> GetCheeses()
        {
            return db.Cheeses;
        }

        // GET: api/CheeseApi/5
        [ResponseType(typeof(Cheese))]
        public async Task<IHttpActionResult> GetCheese(int id)
        {
            Cheese cheese = await db.Cheeses.FindAsync(id);
            if (cheese == null)
            {
                return NotFound();
            }

            return Ok(cheese);
        }

        // PUT: api/CheeseApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutCheese(int id, Cheese cheese)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cheese.Id)
            {
                return BadRequest();
            }

            db.Entry(cheese).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheeseExists(id))
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

        // POST: api/CheeseApi
        [ResponseType(typeof(Cheese))]
        public async Task<IHttpActionResult> PostCheese(Cheese cheese)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cheeses.Add(cheese);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = cheese.Id }, cheese);
        }

        // DELETE: api/CheeseApi/5
        [ResponseType(typeof(Cheese))]
        public async Task<IHttpActionResult> DeleteCheese(int id)
        {
            Cheese cheese = await db.Cheeses.FindAsync(id);
            if (cheese == null)
            {
                return NotFound();
            }

            db.Cheeses.Remove(cheese);
            await db.SaveChangesAsync();

            return Ok(cheese);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CheeseExists(int id)
        {
            return db.Cheeses.Count(e => e.Id == id) > 0;
        }
    }
}
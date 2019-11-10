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
using beeftechee.Entities;

namespace beeftechee.REST_Controllers
{
    public class DrinkApiController : ApiController
    {
        private BeeftecheeDb db = new BeeftecheeDb();

        // GET: api/DrinkApi
        public IQueryable<Drink> GetDrinks()
        {
            return db.Drinks;
        }

        // GET: api/DrinkApi/5
        [ResponseType(typeof(Drink))]
        public async Task<IHttpActionResult> GetDrink(int id)
        {
            Drink drink = await db.Drinks.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            return Ok(drink);
        }

        // PUT: api/DrinkApi/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDrink(int id, Drink drink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != drink.Id)
            {
                return BadRequest();
            }

            db.Entry(drink).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DrinkExists(id))
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

        // POST: api/DrinkApi
        [ResponseType(typeof(Drink))]
        public async Task<IHttpActionResult> PostDrink(Drink drink)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Drinks.Add(drink);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = drink.Id }, drink);
        }

        // DELETE: api/DrinkApi/5
        [ResponseType(typeof(Drink))]
        public async Task<IHttpActionResult> DeleteDrink(int id)
        {
            Drink drink = await db.Drinks.FindAsync(id);
            if (drink == null)
            {
                return NotFound();
            }

            db.Drinks.Remove(drink);
            await db.SaveChangesAsync();

            return Ok(drink);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DrinkExists(int id)
        {
            return db.Drinks.Count(e => e.Id == id) > 0;
        }
    }
}
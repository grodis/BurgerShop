using beeftechee.Database;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

namespace beeftechee.Services
{
    public class IngredientServices
    {
        public async Task<T> GetAsync<T>(object id) where T : class
        {
            try
            {
                using (BeeftecheeDb context = new BeeftecheeDb())
                {

                    DbSet<T> dbSet = context.Set<T>();
                    return await dbSet.FindAsync(id);
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid Entity", ex);
            }
        }

        public async Task<List<T>> GetAllAsync<T>() where T : class
        {
            try
            {
                using (BeeftecheeDb context = new BeeftecheeDb())
                {
                    DbSet<T> dbSet = context.Set<T>();
                    return await dbSet.ToListAsync();
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid Entity", ex);
            }

        }

        public async Task AddAsync<T>(T entity) where T : class
        {
            try
            {
                using (BeeftecheeDb context = new BeeftecheeDb())
                {
                  
                    context.Entry(entity).State = EntityState.Added;
                    await context.SaveChangesAsync();
                    
                   
                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid Entity", ex);
            }

        }

        public async Task UpdateAsync<T>(T entity) where T : class
        {
            try
            {
                using (BeeftecheeDb context = new BeeftecheeDb())
                {

                    context.Entry(entity).State = EntityState.Modified;
                    await context.SaveChangesAsync();


                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid Entity", ex);
            }

        }

        public async Task DeleteAsync<T>(T entity) where T : class
        {
            try
            {
                using (BeeftecheeDb context = new BeeftecheeDb())
                {

                    context.Entry(entity).State = EntityState.Deleted;
                    await context.SaveChangesAsync();


                }
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid Entity", ex);
            }

        }
    }
}

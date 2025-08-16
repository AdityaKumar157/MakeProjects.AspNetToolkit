using MakeProjects.AspNetToolkit.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeProjects.AspNetToolkit.Infrastructure.Repositories
{
    /// <summary>
    /// Provides a base implementation of the <see cref="IRepository{TEntity, TKey}"/> interface for managing entities
    /// in a database.
    /// </summary>
    /// <remarks>This class serves as a generic repository for performing common data access operations, such
    /// as retrieving, adding, updating,  and removing entities. It uses Entity Framework's <see cref="DbContext"/> and
    /// <see cref="DbSet{TEntity}"/> to interact with the database.  Derived classes can override the virtual methods to
    /// customize behavior for specific entity types.</remarks>
    /// <typeparam name="TEntity">The type of the entity being managed. Must be a reference type.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for the entity. Must be non-nullable.</typeparam>
    public class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class
        where TKey : notnull
    {
        /// <summary>
        /// Database context used for accessing the database.
        /// </summary>
        protected readonly DbContext _db;

        /// <summary>
        /// Database set for the entity type being managed.
        /// </summary>
        protected readonly DbSet<TEntity> _dbSet;

        /// <summary>
        /// Constructs a new instance of the <see cref="BaseRepository{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        public BaseRepository(DbContext dbContext)
        {
            _db = dbContext;
            _dbSet = _db.Set<TEntity>();
        }

        #region IRepository Members
        /// <inheritdoc/>
        public virtual async Task<TEntity> FindByIdAsync(TKey id) => await _dbSet.FindAsync(id);

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync() => await _dbSet.AsNoTracking().ToListAsync();

        /// <inheritdoc/>
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (!_db.Entry(entity).IsKeySet)
            {
                throw new InvalidOperationException("Entity must have a key set to be updated.");
            }

            try
            {
                _dbSet.Update(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Reload entity values from the database to resolve conflicts
                foreach (var entry in ex.Entries)
                {
                    await entry.ReloadAsync();
                }

                // Try again once after reload
                _dbSet.Update(entity);
                await _db.SaveChangesAsync();
                return entity;
            }

            // If we reach here, it means the update failed after multiple attempts
            throw new InvalidOperationException("Failed to update the entity after multiple attempts due to concurrency issues.");
        }

        /// <inheritdoc/>
        public virtual async Task<bool> RemoveAsync(TKey id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with key {id} not found.");
            }

            // Ensure the entity has a key set before attempting to remove it
            if (!_db.Entry(entity).IsKeySet)
            {
                throw new InvalidOperationException("Entity must have a key set to be removed.");
            }

            // Remove the entity from the DbSet
            _dbSet.Remove(entity);
            await _db.SaveChangesAsync();
            return true;
        }
        #endregion IRepository Members
    }
}

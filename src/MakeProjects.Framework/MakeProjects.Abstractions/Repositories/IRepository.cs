using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeProjects.Abstractions.Repositories
{
    /// <summary>
    /// Defines a generic repository for managing entities of type <typeparamref name="TEntity"/>  with keys of type
    /// <typeparamref name="TKey"/>.
    /// </summary>
    /// <remarks>This interface provides asynchronous methods for common data access operations,  including
    /// retrieving, adding, updating, and removing entities. It is designed to  abstract the underlying data storage
    /// mechanism, enabling a consistent API for  working with entities.</remarks>
    /// <typeparam name="TEntity">The type of the entity managed by the repository. Must be a reference type.</typeparam>
    /// <typeparam name="TKey">The type of the key used to uniquely identify entities. Must be non-nullable.</typeparam>
    public interface IRepository<TEntity, TKey>
        where TEntity : class
        where TKey : notnull
    {
        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier.
        /// </summary>
        /// <remarks>This method performs an asynchronous lookup for an entity using the provided
        /// identifier. If no entity with the specified identifier exists, the method returns <see
        /// langword="null"/>.</remarks>
        /// <param name="id">The unique identifier of the entity to retrieve. Must not be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity if found; otherwise,
        /// <see langword="null"/>.</returns>
        Task<TEntity> FindByIdAsync(TKey id);

        /// <summary>
        /// Asynchronously retrieves all entities of type <typeparamref name="TEntity"/> from the data source.
        /// </summary>
        /// <remarks>The method does not guarantee the order of the returned entities. Ensure that the
        /// caller handles any necessary filtering or sorting.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/>
        /// of <typeparamref name="TEntity"/> representing all entities in the data source. If no entities are found,
        /// the result is an empty collection.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Asynchronously adds the specified entity to the data store.
        /// </summary>
        /// <param name="entity">The entity to add. Cannot be <see langword="null"/>.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the added entity.</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Updates the specified entity in the data store asynchronously.
        /// </summary>
        /// <remarks>The entity must already exist in the data store. Ensure that the entity's key or
        /// identifier is valid and corresponds to an existing record. The method performs an update operation and
        /// returns the updated entity upon successful completion.</remarks>
        /// <param name="entity">The entity to update. Must not be <see langword="null"/>.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity.</returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Removes the entity with the specified identifier asynchronously.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to remove.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the entity
        /// was successfully removed; otherwise, <see langword="false"/> if the entity was not found or could not be
        /// removed.</returns>
        Task<bool> RemoveAsync(TKey id);
    }
}

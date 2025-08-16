using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeProjects.AspNetToolkit.Abstractions.Services
{
    /// <summary>
    /// Defines a service for performing Create, Read, Update, and Delete (CRUD) operations on entities of type <typeparamref name="TEntity"/>.
    /// </summary>
    /// <remarks>This interface provides asynchronous methods for retrieving, creating, updating, and deleting
    /// entities. Implementations of this interface are expected to handle the persistence and retrieval of
    /// entities.</remarks>
    /// <typeparam name="TEntity">The type of the entity managed by the service.</typeparam>
    /// <typeparam name="TKey">The type of the unique identifier (primary key type) for the entity.</typeparam>
    public interface ICrudService<TEntity, TKey> : IService
    {
        /// <summary>
        /// Asynchronously retrieves all entities of type <typeparamref name="TEntity"/> from the data source.
        /// </summary>
        /// <remarks>This method is typically used to retrieve all records of a specific entity type from
        /// a repository or database. The caller is responsible for awaiting the returned task to ensure the operation
        /// completes.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task result contains an <see cref="IEnumerable{T}"/>
        /// of <typeparamref name="TEntity"/> representing all entities in the data source. If no entities are found,
        /// the result will be an empty collection.</returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier.
        /// </summary>
        /// <remarks>This method performs an asynchronous operation to fetch an entity from the underlying
        /// data source using the provided identifier. If no entity with the specified identifier exists, the method
        /// returns <see langword="null"/>.</remarks>
        /// <param name="id">The unique identifier of the entity to retrieve. Must not be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the entity of type <typeparamref
        /// name="TEntity"/>  if found; otherwise, <see langword="null"/>.</returns>
        Task<TEntity?> GetByIdAsync(TKey id);

        /// <summary>
        /// Asynchronously creates a new entity in the data store.
        /// </summary>
        /// <remarks>This method performs an asynchronous operation to create an entity from the underlying
        /// data source using the provided entity information.</remarks>
        /// <param name="entity">The entity to be created. This parameter cannot be <see langword="null"/>.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the created entity of type <typeparamref
        /// name="TEntity"/>.</returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// Updates the specified entity in the data store asynchronously.
        /// </summary>
        /// <remarks>This method performs an asynchronous operation to update an entity from the underlying
        /// data source using the provided entity information.</remarks>
        /// <param name="entity">The entity to update. Must not be <see langword="null"/>.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the updated entity if the update
        /// is successful; otherwise, <see langword="null"/> if the entity does not exist or the update fails.</returns>
        Task<TEntity?> UpdateAsync(TEntity entity);

        /// <summary>
        /// Asynchronously deletes an entity identified by the specified key.
        /// </summary>
        /// <remarks>This method performs an asynchronous operation to delete an entity from the underlying
        /// data source using the provided identifier.</remarks>
        /// <param name="id">The unique identifier of the entity to delete. Must not be null.</param>
        /// <returns>A task that represents the asynchronous operation. The task result is <see langword="true"/> if the entity
        /// was successfully deleted; otherwise, <see langword="false"/>.</returns>
        Task<bool> DeleteAsync(TKey id);
    }
}

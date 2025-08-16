using MakeProjects.Abstractions.Repositories;
using MakeProjects.Abstractions.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MakeProjects.Abstractions.Exceptions.Exceptions;

namespace MakeProjects.Infrastructure.Services
{
    /// <summary>
    /// Provides a base implementation of <see cref="ICrudService{TEntity, TKey}"/> for CRUD (Create, Read, Update, Delete) operations on entities.
    /// </summary>
    /// <remarks>This class serves as a foundational service for managing entities in a repository. It
    /// provides common CRUD operations such as creating, retrieving, updating, and deleting entities. The service
    /// relies on an <see cref="IRepository{TEntity, TKey}"/> for data access and an <see
    /// cref="ILogger{TCategoryName}"/> for logging operations. Derived classes can override the virtual methods to
    /// customize behavior.</remarks>
    /// <typeparam name="TEntity">The type of the entity that this service operates on. Must be a reference type.</typeparam>
    /// <typeparam name="TKey">The type of the primary key for the entity. Must be non-nullable.</typeparam>
    public class BaseCrudService<TEntity, TKey> : ICrudService<TEntity, TKey>
        where TEntity : class
        where TKey : notnull
    {
        #region Protected Members
        protected readonly IRepository<TEntity, TKey> _repository;
        protected readonly ILogger<BaseCrudService<TEntity, TKey>> _logger;

        protected string? _description;
        #endregion Protected Members

        #region Constructors
        /// <summary>
        /// Constructs a new instance of the <see cref="BaseCrudService{TEntity, TKey}"/> class.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="logger"></param>
        public BaseCrudService(IRepository<TEntity, TKey> repository, ILogger<BaseCrudService<TEntity, TKey>> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <summary>
        /// Constructs a new instance of the <see cref="BaseCrudService{TEntity, TKey}"/> class with a description.
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="description"></param>
        /// <param name="logger"></param>
        public BaseCrudService(IRepository<TEntity, TKey> repository, string description, ILogger<BaseCrudService<TEntity, TKey>> logger)
        {
            _repository = repository;
            _description = description;
            _logger = logger;
        }
        #endregion Constructors

        #region IService Members
        /// <inheritdoc/>
        public string Description => _description ?? string.Empty;
        #endregion IService Members

        #region ICrudService Members
        /// <inheritdoc/>
        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            try
            {
                _logger.LogTrace(FormattableString.Invariant($"ENTRY: {nameof(BaseCrudService<TEntity, TKey>)}.CreateAsync(TEntity entity) for entity={typeof(TEntity).Name}."));

                if (entity == null)
                {
                    _logger.LogTrace("Entity is null.");
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
                }

                return await _repository.AddAsync(entity) ?? throw new InvalidOperationException(FormattableString.Invariant($"Failed to create entity of type {typeof(TEntity).Name}."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, FormattableString.Invariant($"Exception in {nameof(BaseCrudService<TEntity, TKey>)}.CreateAsync(TEntity entity) for entity={typeof(TEntity).Name}."));
                throw;
            }
            finally
            {
                _logger.LogTrace(FormattableString.Invariant($"EXIT: {nameof(BaseCrudService<TEntity, TKey>)}.CreateAsync(TEntity entity) for entity={typeof(TEntity).Name}."));
            }
        }

        /// <inheritdoc/>
        public virtual async Task<bool> DeleteAsync(TKey id)
        {
            try
            {
                _logger.LogTrace(FormattableString.Invariant($"ENTRY: {nameof(BaseCrudService<TEntity, TKey>)}.DeleteAsync(TKey id) for entity={typeof(TEntity).Name}. id={id}"));

                if (string.IsNullOrEmpty(id.ToString()))
                {
                    _logger.LogTrace("Entity key is null or empty.");
                    throw new ArgumentNullException(nameof(id), "Entity primary key cannot be null");
                }

                return await _repository.RemoveAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, FormattableString.Invariant($"Exception in {nameof(BaseCrudService<TEntity, TKey>)}.DeleteAsync(TKey id) for entity={typeof(TEntity).Name}. id={id}"));
                throw;
            }
            finally
            {
                _logger.LogTrace(FormattableString.Invariant($"EXIT: {nameof(BaseCrudService<TEntity, TKey>)}.DeleteAsync(TKey id) for entity={typeof(TEntity).Name}. id={id}"));
            }
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            try
            {
                _logger.LogTrace(FormattableString.Invariant($"ENTRY: {nameof(BaseCrudService<TEntity, TKey>)}.GetAllAsync() for entity={typeof(TEntity).Name}."));
                return await _repository.GetAllAsync() ?? throw new NotFoundException(FormattableString.Invariant($"No entities of type {typeof(TEntity).Name} found."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, FormattableString.Invariant($"Exception in {nameof(BaseCrudService<TEntity, TKey>)}.GetAllAsync() for entity={typeof(TEntity).Name}."));
                throw;
            }
            finally
            {
                _logger.LogTrace(FormattableString.Invariant($"EXIT: {nameof(BaseCrudService<TEntity, TKey>)}.GetAllAsync() for entity={typeof(TEntity).Name}."));
            }
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity?> GetByIdAsync(TKey id)
        {
            try
            {
                _logger.LogTrace(FormattableString.Invariant($"ENTRY: {nameof(BaseCrudService<TEntity, TKey>)}.GetByIdAsync(TKey id) for entity={typeof(TEntity).Name}. id={id}"));

                if (string.IsNullOrEmpty(id.ToString()))
                {
                    _logger.LogTrace("Entity key is null or empty.");
                    throw new ArgumentNullException(nameof(id), "Entity primary key cannot be null");
                }

                return await _repository.FindByIdAsync(id) ?? throw new NotFoundException(FormattableString.Invariant($"Entity of type {typeof(TEntity).Name} with ID {id} not found."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, FormattableString.Invariant($"Exception in {nameof(BaseCrudService<TEntity, TKey>)}.GetByIdAsync(TKey id) for entity={typeof(TEntity).Name}. id={id}"));
                throw;
            }
            finally
            {
                _logger.LogTrace(FormattableString.Invariant($"EXIT: {nameof(BaseCrudService<TEntity, TKey>)}.GetByIdAsync(TKey id) for entity={typeof(TEntity).Name}. id={id}"));
            }
        }

        /// <inheritdoc/>
        public virtual async Task<TEntity?> UpdateAsync(TEntity entity)
        {
            try
            {
                _logger.LogTrace(FormattableString.Invariant($"ENTRY: {nameof(BaseCrudService<TEntity, TKey>)}.UpdateAsync(TEntity entity) for entity={typeof(TEntity).Name}."));

                if (entity == null)
                {
                    _logger.LogTrace("Entity is null.");
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null");
                }

                return await _repository.UpdateAsync(entity) ?? throw new NotFoundException(FormattableString.Invariant($"Entity of type {typeof(TEntity).Name} not found for update."));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, FormattableString.Invariant($"Exception in {nameof(BaseCrudService<TEntity, TKey>)}.UpdateAsync(TEntity entity) for entity={typeof(TEntity).Name}."));
                throw;
            }
            finally
            {
                _logger.LogTrace(FormattableString.Invariant($"EXIT: {nameof(BaseCrudService<TEntity, TKey>)}.UpdateAsync(TEntity entity) for entity={typeof(TEntity).Name}."));
            }
        }
        #endregion ICrudService Members
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeProjects.Abstractions.Repositories
{
    /// <summary>
    /// Defines a contract for managing database transactions and persisting changes to the database.
    /// </summary>
    /// <remarks>The <see cref="IUnitOfWork"/> interface provides methods for handling database transactions
    /// and  saving changes in a consistent and atomic manner. It is typically used to implement the Unit of Work 
    /// design pattern, ensuring that multiple operations are treated as a single transaction.  Implementations of this
    /// interface should ensure proper resource management, including disposing of  any underlying resources when the
    /// instance is no longer needed.</remarks>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Asynchronously saves all changes made in the current context to the underlying database.
        /// </summary>
        /// <remarks>This method commits all tracked changes to the database, including added, modified,
        /// and deleted entities. If the operation is canceled via the <paramref name="cancellationToken"/>, the
        /// returned task will be in a canceled state.</remarks>
        /// <param name="cancellationToken">A <see cref="CancellationToken"/> that can be used to cancel the save operation.</param>
        /// <returns>A task that represents the asynchronous save operation. The task result contains the number of state entries
        /// written to the database.</returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Begins a new database transaction asynchronously.
        /// </summary>
        /// <remarks>This method initiates a transaction that can be used to group multiple database
        /// operations into a single unit of work. Ensure that the transaction is committed or rolled back  to finalize
        /// or discard the changes.</remarks>
        /// <returns>A task that represents the asynchronous operation of starting the transaction.</returns>
        Task BeginTransactionAsync();

        /// <summary>
        /// Commits the current transaction asynchronously.
        /// </summary>
        /// <remarks>This method finalizes the transaction, making all changes within the transaction
        /// permanent. Ensure that the transaction is in a valid state before calling this method. If the transaction
        /// has already been committed or rolled back, calling this method may result in an exception.</remarks>
        /// <returns>A task that represents the asynchronous operation. The task completes when the transaction is successfully
        /// committed.</returns>
        Task CommitTransactionAsync();

        /// <summary>
        /// Asynchronously rolls back the current transaction, undoing all changes made during the transaction.
        /// </summary>
        /// <remarks>This method should be called to cancel a transaction if an error occurs or if the
        /// changes made during the transaction should not be committed. After calling this method, the transaction is
        /// no longer valid and cannot be reused.</remarks>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task RollbackTransactionAsync();
    }
}

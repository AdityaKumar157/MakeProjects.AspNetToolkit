using MakeProjects.Abstractions.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakeProjects.Infrastructure.Repositories
{
    /// <summary>
    /// Provides a unit of work implementation for managing database transactions and changes.
    /// </summary>
    /// <remarks>The <see cref="UnitOfWork"/> class encapsulates a <see cref="DbContext"/> to manage database
    /// operations  within a transactional boundary. It ensures that all changes to the database are committed or rolled
    /// back  as a single unit of work. This class implements the <see cref="IUnitOfWork"/> interface for transaction 
    /// management and the <see cref="IDisposable"/> interface for resource cleanup.</remarks>
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private IDbContextTransaction? _transaction;

        /// <summary>
        /// Constructs a new instance of the <see cref="UnitOfWork"/> class with the specified <see cref="DbContext"/>.
        /// </summary>
        /// <param name="dbContext"></param>
        public UnitOfWork(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region IUnitOfWork Members
        /// <inheritdoc/>
        public async Task BeginTransactionAsync()
        {
            _transaction ??= await _dbContext.Database.BeginTransactionAsync();
        }

        /// <inheritdoc/>
        public async Task CommitTransactionAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
                if(_transaction !=null)
                {
                    await _transaction.CommitAsync();
                }
            }
            catch
            {
                if(_transaction != null)
                {
                    await _transaction.RollbackAsync();
                }
                throw; // Re-throw the exception to be handled by the caller
            }
            finally
            {
                if(_transaction != null)
                {
                    await _transaction.DisposeAsync();
                    _transaction = null;
                }
            }
        }

        /// <inheritdoc/>
        public async Task RollbackTransactionAsync()
        {
            if(_transaction !=null)
            {
                await _transaction.RollbackAsync();
                await _transaction.DisposeAsync();
                _transaction = null;
            }
        }

        /// <inheritdoc/>
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
        #endregion IUnitOfWork Members

        #region IDisposable Members
        /// <inheritdoc/>
        public void Dispose()
        {
            _dbContext?.Dispose();
            _transaction?.Dispose();
        }
        #endregion IDisposable Members
    }
}



//DI Registration in RepositoryServiceCollectionExtensions.cs
//services.AddScoped<IUnitOfWork, UnitOfWork>();

/**
✅ Example Usage
public class OrderService
{
    private readonly IRepository<Order, Guid> _orderRepository;
    private readonly IRepository<Product, Guid> _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(
        IRepository<Order, Guid> orderRepository,
        IRepository<Product, Guid> productRepository,
        IUnitOfWork unitOfWork)
    {
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task PlaceOrderAsync(Order order, List<Product> products)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            await _orderRepository.AddAsync(order);

            foreach (var product in products)
            {
                product.Stock -= 1;
                _productRepository.Update(product);
            }

            await _unitOfWork.CommitTransactionAsync();
        }
        catch
        {
            await _unitOfWork.RollbackTransactionAsync();
            throw;
        }
    }
}
*/
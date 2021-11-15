using Domain.OraLounge.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.OraLounge
{
    public interface IUnitOfWork : IDisposable
    {
        #region Properties
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductImageRepository ProductImageRepository { get; }
        IUserRepository UserRepository { get; }
        IBookingRepsitory BookingRepository { get; }
        ITableRepository TableRepository { get; }
        #endregion

        #region Methods
        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        #endregion
    }
}

using Data.OraLounge.Repositories;
using Domain.OraLounge;
using Domain.OraLounge.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.OraLounge
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private IProductImageRepository _productImageRepository;
        private IUserRepository _userRepository;
        private IBookingRepsitory _bookingRepository;
        private ITableRepository _tableRepository;
        #endregion

        #region Constructors
        public UnitOfWork()
        {
            _context = new ApplicationDbContext();
        }
        #endregion

        #region IUnitOfWork Members

        public ICategoryRepository CategoryRepository
        {
            get { return _categoryRepository ?? (_categoryRepository = new CategoryRepository(_context)); }
        }

        public IProductRepository ProductRepository
        {
            get { return _productRepository ?? (_productRepository = new ProductRepository(_context)); }
        }

        public IProductImageRepository ProductImageRepository
        {
            get { return _productImageRepository ?? (_productImageRepository = new ProductImageRepository(_context)); }
        }

        public IUserRepository UserRepository
        {
            get { return _userRepository ?? (_userRepository = new UserRepository(_context)); }
        }

        public IBookingRepsitory BookingRepository 
        {
            get { return _bookingRepository ?? (_bookingRepository = new BookingRepository(_context)); }
        }

        public ITableRepository TableRepository
        {
            get { return _tableRepository ?? (_tableRepository = new TableRepository(_context)); }
        }
        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            _categoryRepository = null;
            _productRepository = null;
            _productImageRepository = null;
            _userRepository = null;
            _context.Dispose();
        }
        #endregion
    }
}

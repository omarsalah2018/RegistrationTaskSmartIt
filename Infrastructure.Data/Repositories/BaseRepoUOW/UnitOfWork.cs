using Infrastructure.Data.Contexts;
using RegistrationTask.Core.Application.Interfaces;

namespace RegistrationTask.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RegistrationDbContext _context;
        public UnitOfWork(RegistrationDbContext context)
        {
            _context = context;
        }
        #region IUnitOfWork Members
        public int Complete()
        {
            if (_context == null)
                throw new InvalidOperationException("Context has not been initialized.");
            return _context.SaveChanges();
        }

        public async Task<int> CompleteAsync()
        {
            if (_context == null)
                throw new InvalidOperationException("Context has not been initialized.");
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
        #endregion
    }
}

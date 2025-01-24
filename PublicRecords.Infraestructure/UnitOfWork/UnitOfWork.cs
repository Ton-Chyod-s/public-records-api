using PublicRecords.Domain.Interface.Repository;
using PublicRecords.Domain.Interface.UnitOfWork;
using PublicRecords.Infraestructure.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace PublicRecords.Infraestructure.UnitOfWork
{
    internal class UnitOfWork(OfficialDiaryDbContext context) : IUnitOfWork
    {
        private readonly OfficialDiaryDbContext _context = context;
        private bool disposed = false;

        #region [Repositories]
        public IPersonRepository PersonRepository { get; private set; } = new Repository.PersonRepository(context);
        public IOfficialDiariesRepository OfficialDiariesRepository { get; private set; } = new Repository.OfficialDiaryRepository(context);
        public ISessionRepository SessionRepository { get; private set; } = new Repository.SessionRepository(context);
        public IUserRepository UserRepository { get; private set; } = new Repository.UserRepository(context);
        public IAuthTokenRepository AuthTokenRepository { get; private set; } = new Repository.AuthTokenRepository(context);
        public IAddOrUpdateLoginRepository CreateOrUpdateLoginRepository { get; private set; } = new Repository.AddOrUpdateLoginRepository(context);
        #endregion

        #region [Methods]
        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed && disposing)
                _context.Dispose();

            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    
    }
}

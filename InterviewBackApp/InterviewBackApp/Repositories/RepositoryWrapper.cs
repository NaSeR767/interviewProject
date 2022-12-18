using InterviewBackApp.Data;
using InterviewBackApp.Repositories.User;

namespace InterviewBackApp.Repositories
{
    public class RepositoryWrapper: IRepositoryWrapper
    {
        private BackendContext _repoContext;

        public RepositoryWrapper(BackendContext repositoryContext) 
        {
            _repoContext = repositoryContext;
        }

        private IUserRepository _userRepository;

        public IUserRepository UserRepository
        {
            get
            {
                if (_userRepository == null)
                {
                    _userRepository = new UserRepository(_repoContext);
                }

                return _userRepository;
            }
        }


        public async Task SaveAsync()
        {
            await _repoContext.SaveChangesAsync();
        }

        public void Save()
        {
            _repoContext.SaveChanges();
        }


        // **********
        /// <summary>
        /// To detect redundant calls
        /// </summary>
        public bool IsDisposed { get; protected set; }


        // **********

        /// <summary>
        /// Public implementation of Dispose pattern callable by consumers.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            System.GC.SuppressFinalize(this);
        }

        /// <summary>
        /// https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
            {
                return;
            }

            if (disposing)
            {
                // TODO: dispose managed state (managed objects).

                if (_repoContext != null)
                {
                    _repoContext.Dispose();
                    _repoContext = null;
                }
            }

            // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
            // TODO: set large fields to null.

            IsDisposed = true;
        }


    }
}

using InterviewBackApp.Repositories.User;

namespace InterviewBackApp.Repositories
{
    public interface IRepositoryWrapper: IDisposable
    {

        IUserRepository UserRepository { get; }

        void Save();
        Task SaveAsync();

        bool IsDisposed { get; }
    }
}

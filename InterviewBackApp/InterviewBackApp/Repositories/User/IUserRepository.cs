using InterviewBackApp.Models;
using InterviewBackApp.ViewModels;

namespace InterviewBackApp.Repositories.User
{
    public interface IUserRepository: IRepositoryBase<Models.User>
    {
        Task<Models.User> CreateUser(RequestCreateUser request);
        Task<Models.User> GetUserById(Guid userId);
        Task<Models.User> UpdateUser(Models.User user, RequestUpdateUser request);
        Task<IList<UserToList>> GetPagination(int pageNumber, int take);
        Task<int> GetPaginationCount();
        Task DeleteUser(Models.User user);
    }
}

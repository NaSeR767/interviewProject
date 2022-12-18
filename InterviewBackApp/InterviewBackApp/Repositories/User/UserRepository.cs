using InterviewBackApp.Data;
using InterviewBackApp.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InterviewBackApp.Repositories.User
{
    public class UserRepository : RepositoryBase<Models.User>, IUserRepository
    {
        public UserRepository(BackendContext repositoryContext) : base(repositoryContext)
        {
        }


        public async Task<Models.User> CreateUser(RequestCreateUser request)
        {
            var newUser = new Models.User()
            {
                Name = request.Name,
                Email = request.Email,
                IsActive = true,
            };

            await CreateAsync(newUser);

            return newUser;
        }

        public async Task<Models.User> GetUserById(Guid userId)
        {
            return
                await
                GetByQuery()
                .Where(current => current.Id == userId)
                .FirstOrDefaultAsync();
        }

        public async Task<Models.User> UpdateUser(Models.User user, RequestUpdateUser request)
        {
            
            user.Name = request.Name;
            user.Email = request.Email;

            await UpdateAsync(user);

            return user;
        }

        public async Task<IList<UserToList>> GetPagination(int pageNumber, int take)
        {
            return
                await
                GetByQuery()
                .OrderByDescending(current => current.CreateAt)
                .Skip((pageNumber - 1) * take)
                .Take(take)
                .Select(current => new UserToList
                {
                    Id = current.Id,
                    Name = current.Name,
                })
                .ToListAsync();
        }

        public async Task<int> GetPaginationCount()
        {
            return
                await
                GetByQuery()
                .CountAsync();
        }

        public async Task DeleteUser(Models.User user)
        {

            await DeleteAsync(user);

        }

    }
}

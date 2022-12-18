using InterviewBackApp.Repositories;
using InterviewBackApp.ViewModels;
using InterviewBackApp.ViewModels.Result;
using Microsoft.AspNetCore.Mvc;

namespace InterviewBackApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        public UserController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<ResultViewModelWithDataModel<ResponseCreateUser>>> Create([FromBody] RequestCreateUser request)
        {
            
            var response = new ResultViewModelWithDataModel<ResponseCreateUser>();
            response.Data = new ResponseCreateUser();

            try
            {
                var newUser =
                    await
                    _repositoryWrapper
                    .UserRepository
                    .CreateUser(request: request);

                await _repositoryWrapper.SaveAsync();
                
                response.IsSuccess = true;

                response.Data.User = newUser;

                return Ok(response);
            }
            catch (Exception)
            {
                response.Data = null;
                response.IsSuccess = false;
                response.ErrorMessages.Add("خطایی رخ داده است");

                return BadRequest(response);
                throw;

            }
            finally
            {
                _repositoryWrapper.Dispose();
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult<ResultViewModelWithDataModel<ResponseUpdateUser>>> Update([FromBody] RequestUpdateUser request)
        {

            var response = new ResultViewModelWithDataModel<ResponseUpdateUser>();
            response.Data = new ResponseUpdateUser();

            try
            {

                var user =
                    await
                    _repositoryWrapper
                    .UserRepository
                    .GetUserById(userId: request.Id);

                if(user is null)
                {
                    response.ErrorMessages.Add("کاربر یافت نشد");
                    return BadRequest(response);
                }

                var updatedUser =
                    await
                    _repositoryWrapper
                    .UserRepository
                    .UpdateUser(user: user, request: request);

                await _repositoryWrapper.SaveAsync();

                response.IsSuccess = true;

                response.Data.User = updatedUser;

                return Ok(response);
            }
            catch (Exception)
            {
                response.Data = null;
                response.IsSuccess = false;
                response.ErrorMessages.Add("خطایی رخ داده است");

                return BadRequest(response);
                throw;

            }
            finally
            {
                _repositoryWrapper.Dispose();
            }
        }

        [HttpGet("[action]/{id}")]
        public async Task<ActionResult<ResultViewModelWithDataModel<ResponseGetUser>>> Get([FromRoute] Guid id)
        {

            var response = new ResultViewModelWithDataModel<ResponseGetUser>();
            response.Data = new ResponseGetUser();

            try
            {
                var user =
                    await
                    _repositoryWrapper
                    .UserRepository
                    .GetUserById(userId: id);

                if(user is null)
                {
                    response.ErrorMessages.Add("کاربر یافت نشد");
                    return BadRequest(response);
                }

                response.IsSuccess = true;

                response.Data.User = user;

                return Ok(response);
            }
            catch (Exception)
            {
                response.Data = null;
                response.IsSuccess = false;
                response.ErrorMessages.Add("خطایی رخ داده است");

                return BadRequest(response);
                throw;

            }
            finally
            {
                _repositoryWrapper.Dispose();
            }
        }

        [HttpGet("[action]/{pageNumber}/{take}")]
        public async Task<ActionResult<ResultViewModelWithDataModel<ResponseGetUserPagination>>> GetPagination([FromRoute] int pageNumber, [FromRoute] int take)
        {

            var response = new ResultViewModelWithDataModel<ResponseGetUserPagination>();
            response.Data = new ResponseGetUserPagination();

            try
            {

                response.Data.Users =
                    await
                    _repositoryWrapper
                    .UserRepository
                    .GetPagination(pageNumber: pageNumber, take: take);

                response.Data.Count =
                    await
                    _repositoryWrapper
                    .UserRepository
                    .GetPaginationCount();

                response.IsSuccess = true;

                return Ok(response);
            }
            catch (Exception)
            {
                response.Data = null;
                response.IsSuccess = false;
                response.ErrorMessages.Add("خطایی رخ داده است");

                return BadRequest(response);
                throw;

            }
            finally
            {
                _repositoryWrapper.Dispose();
            }
        }

        [HttpDelete("[action]/{id}")]
        public async Task<ActionResult<ResultViewModel>> Delete([FromRoute] Guid id)
        {

            var response = new ResultViewModel();

            try
            {
                var user =
                    await
                    _repositoryWrapper
                    .UserRepository
                    .GetUserById(userId: id);

                if (user is null)
                {
                    response.ErrorMessages.Add("کاربر یافت نشد");
                    return BadRequest(response);
                }

                await
                    _repositoryWrapper
                    .UserRepository
                    .DeleteUser(user);

                await _repositoryWrapper.SaveAsync();

                response.IsSuccess = true;

                return Ok(response);
            }
            catch (Exception)
            {
                response.IsSuccess = false;
                response.ErrorMessages.Add("خطایی رخ داده است");

                return BadRequest(response);
                throw;

            }
            finally
            {
                _repositoryWrapper.Dispose();
            }
        }


    }
}

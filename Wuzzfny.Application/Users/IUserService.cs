using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wuzzfny.Application.Users.Dtos;
using Wuzzfny.Domain.Common;

namespace Wuzzfny.Application.Users
{
    public interface IUserService
    {
        Task<ReturnResultDto<ReturnListDto<UserDto>>> GetAll(SearchDto<UserDto> search);
        Task<ReturnResultDto<ReturnListDto<UserDto>>> GetAllForSelect();
        Task<ReturnResultDto<UserDto>> GetByIdAsync(long id);
        Task<ReturnResultDto<long>> AddUser(UserDto model);
        //Edit the User Profile
        Task<ReturnResultDto<long>> EditUser(long Id, UserDto model);
        Task<ReturnResultDto<long>> SoftDeleteUser(long Id);
    }
}

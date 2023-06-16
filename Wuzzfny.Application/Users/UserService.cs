using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Wuzzfny.Application.Users.Dtos;
using Wuzzfny.Domain.Common;
using Wuzzfny.Domain.Models.Users;

namespace Wuzzfny.Application.Users
{
    public class UserService: IUserService
    {
        private readonly RepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        
        public UserService(IMapper mapper, RepositoryWrapper repositoryWrapper) {
            _mapper = mapper;
            _repositoryWrapper = repositoryWrapper;
        }

        //Register for Admin
        public async Task<ReturnResultDto<long>> AddUser(UserDto model) 
        {
            using (var transaction = await _repositoryWrapper.UserRepository.BeginTransaction())
            {
                var result = new ReturnResultDto<long>();
                try
                {
                    byte[] passwordHash, passwordSalt;
                    var obj = new User();
                    bool IsPhoneAlreadyRegistered = await _repositoryWrapper.UserRepository.AnyAsync(item => item.PhoneNumber == model.PhoneNumber);
                    if (IsPhoneAlreadyRegistered)
                    {
                        await transaction.RollbackAsync();
                        result.httpStatusCode = HttpStatusCode.BadRequest;
                        result.Errors.Add("Mobile Number '" + model.PhoneNumber + "' is already taken.");
                        return result;
                    }
                    bool IsEmailAlreadyRegistered = await _repositoryWrapper.UserRepository.AnyAsync(item => item.Email == model.Email);
                    if (IsEmailAlreadyRegistered)
                    {
                        await transaction.RollbackAsync();
                        result.httpStatusCode = HttpStatusCode.BadRequest;
                        result.Errors.Add("Email'" + model.Email + "' is already taken.");
                        return result;
                    }

                    AuthenticationHelper.CreatePasswordHash(model.Password, out passwordHash, out passwordSalt);
                    var userId = await _repositoryWrapper.UserRepository.AddAsync(new User()
                    {
                        Name = model.Name,
                        Email = model.Email,
                        PhoneNumber = model.PhoneNumber,
                        Password = passwordHash,
                        Salt = passwordSalt,
                        CreationTime = DateTime.Now,
                        Role = UserRoles.Admin,
                    });
                    await transaction.CommitAsync();

                    result.SuccessMessage = "Admin is Created Successfully";

                    return result;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    result.httpStatusCode = HttpStatusCode.BadRequest;
                    result.Errors.Add(ex.Message);
                    return result;
                }
            }
        }

        public Task<ReturnResultDto<long>> EditUser(long Id, UserDto model)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnResultDto<ReturnListDto<UserDto>>> GetAll(SearchDto<UserDto> search)
        {
            throw new NotImplementedException();
        }

        public async Task<ReturnResultDto<ReturnListDto<UserDto>>> GetAllForSelect()
        {
            var result = new ReturnResultDto<ReturnListDto<UserDto>>() { Data = new ReturnListDto<UserDto>() };

            var data = await _repositoryWrapper.UserRepository.GetAllAsync();
            result.Data = _mapper.Map<ReturnListDto<UserDto>>(data);
            result.SuccessMessage = "Data is returned";

            return result;
        }

        public Task<ReturnResultDto<UserDto>> GetByIdAsync(long id)
        {
            throw new NotImplementedException();
        }

        public Task<ReturnResultDto<long>> SoftDeleteUser(long Id)
        {
            throw new NotImplementedException();
        }
    }
}

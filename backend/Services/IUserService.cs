using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Dto;
using backend.Models;

namespace backend.Services
{
    public interface IUserService
    {
        Task<UserDto> Authenticate(string userName, string password);
        Task<UserDto> CreateUser(string userName, string password);
        Task<List<UserDto>> GetAll();
    }
}
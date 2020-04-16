using System.Collections.Generic;
using System.Threading.Tasks;
using backend.Dto;
using backend.Models;

namespace backend.Services
{
    public interface IUserService
    {
        Task<User> Authenticate(UserDto user);
        Task<User> CreateUser(UserDto user);
        Task<List<User>> GetAll();
    }
}
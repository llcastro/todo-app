using AutoMapper;
using backend.Dto;

namespace backend.Models
{
    [AutoMap(typeof(UserDto))]
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
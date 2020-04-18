using AutoMapper;
using backend.Models;

namespace backend.Dto
{
    [AutoMap(typeof(User))]
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
    }
}
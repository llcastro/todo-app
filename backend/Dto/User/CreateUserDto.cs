using System.ComponentModel.DataAnnotations;
using AutoMapper;
using backend.Models;

namespace backend.Dto
{
    [AutoMap(typeof(User))]
    public class CreateUserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
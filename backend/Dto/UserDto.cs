using System.ComponentModel.DataAnnotations;

namespace backend.Dto
{
    public class UserDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
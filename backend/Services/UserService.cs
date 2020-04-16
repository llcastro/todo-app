using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using backend.Dto;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using todo_app.Models;

namespace backend.Services
{
  public class UserService : IUserService
  {
    private readonly TodoContext _context;
    private readonly IConfiguration _configuration;
    public UserService(TodoContext context, IConfiguration configuration)
    {
      _context = context;
      _configuration = configuration;
    }
    public async Task<User> Authenticate(UserDto userDto)
    {
      var user = await _context.Users.SingleOrDefaultAsync(e => e.Name == userDto.UserName && e.Password == userDto.Password);

      // return null if user not found
      if (user == null)
        return null;

      // authentication successful so generate jwt token
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes(_configuration["Security:Tokens:Key"]);
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[] 
        {
          new Claim(ClaimTypes.Name, user.Name.ToString())
        }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      user.Token = tokenHandler.WriteToken(token);

      return user;
    }

    public async Task<User> CreateUser(UserDto user)
    {
      var u = new User()
      {
        Name = user.UserName,
        Password = user.Password,
      };

      _context.Users.Add(u);
      await _context.SaveChangesAsync();

      return u;
    }

    public async Task<List<User>> GetAll()
    {
      var user = await _context.Users.ToListAsync();

      return user;
    }
  }
}
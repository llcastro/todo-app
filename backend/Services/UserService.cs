using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using backend.CustomExceptions;
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
    private readonly IMapper _mapper;
    public UserService(TodoContext context, IConfiguration configuration, IMapper mapper)
    {
      _context = context;
      _configuration = configuration;
      _mapper = mapper;
    }
    public async Task<UserDto> Authenticate(string userName, string password)
    {
      var user = await _context.Users.SingleOrDefaultAsync(e => e.UserName == userName && e.Password == password);

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
          new Claim(ClaimTypes.Name, user.UserName.ToString())
        }),
        Expires = DateTime.UtcNow.AddDays(7),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };
      var token = tokenHandler.CreateToken(tokenDescriptor);
      user.Token = tokenHandler.WriteToken(token);

      return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> CreateUser(string userName, string password)
    {
      var user = await _context.Users.SingleOrDefaultAsync(e => e.UserName == userName);

      if (user != null)
        throw new UserFriendlyException("UserName already exists!", HttpStatusCode.Conflict);

      user = new User()
      {
        UserName = userName,
        Password = password,
      };

      _context.Users.Add(user);
      await _context.SaveChangesAsync();

      return _mapper.Map<UserDto>(user);
    }

    public async Task<List<UserDto>> GetAll()
    {
      var user = await _context.Users.ToListAsync();

      return _mapper.Map<List<UserDto>>(user);
    }
  }
}
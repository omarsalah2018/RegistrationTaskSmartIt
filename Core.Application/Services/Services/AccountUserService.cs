using Core.Application.Dtos;
using Core.Application.Interfaces.ICustomRepo;
using Core.Application.Services.IServices;
using Core.Domain.Entities;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using RegistrationTask.Core.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RegistrationTask.Core.Application.Services.Service
{
    public class AccountUserService : IAccountUserService
    {
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration iconfiguration;
        private readonly ILogger<AccountUserService> _logger;

        public AccountUserService(IUserService userService, IUserRepository userRepository, IUnitOfWork unitOfWork, IConfiguration iconfiguration, ILogger<AccountUserService> logger)
        {
            _userService = userService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            this.iconfiguration = iconfiguration;
            _logger = logger;
        }

       
        public string GenerateToken(UserDto user)
        {
            try
            {
                var token = "";
                if (user != null)
                {
                    // Else we generate JSON Web Token
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.UTF8.GetBytes(iconfiguration["JWT:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]{
                                  new Claim(ClaimTypes.Name, user.UserName),
              new Claim(ClaimTypes.Email, user.Email),
              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                  }),
                        Expires = DateTime.UtcNow.AddDays(10),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                    };
                    var tokenJSONCreated = tokenHandler.CreateToken(tokenDescriptor);
                    token = tokenHandler.WriteToken(tokenJSONCreated);
                }
                return token;
            }
            catch (Exception e)
            {
                _logger.LogError(0, e.StackTrace);
                throw;
            }
        }



        public UserDto Login(string email, string password)
        {
            try
            {
                UserDto userDto = new UserDto();
                User user = _userRepository.GetUserByEmailAndPassword(email, password);
                if (user == null) return null;

                userDto = user.Adapt(userDto);
                return userDto;

            }
            catch (Exception e)
            {
                _logger.LogError(0, e.StackTrace);
                throw;
            }
        }
    }
}

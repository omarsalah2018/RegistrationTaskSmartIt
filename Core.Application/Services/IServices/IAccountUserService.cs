using Core.Application.Dtos;

namespace Core.Application.Services.IServices
{
    public interface IAccountUserService
    {
        UserDto Login(string email, string password);
        string GenerateToken(UserDto user);
    }
}

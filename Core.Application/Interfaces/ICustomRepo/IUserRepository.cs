using Core.Domain.Entities;
using RegistrationTask.Core.Application.Interfaces;

namespace Core.Application.Interfaces.ICustomRepo
{
    public interface IUserRepository :IBaseRepository<User>
    {
        List<User> GetAllRequests();
        User GetUserByEmailAndPassword(string email, string password);
        User GetByEmail(string email);
        List<User> GetNotApprovedRejectedRequestsAfterThreeDays();
    }
}

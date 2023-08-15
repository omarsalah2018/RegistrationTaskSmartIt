using Core.Application.Enums;
using Core.Application.Interfaces.ICustomRepo;
using Core.Domain.Entities;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using RegistrationTask.Infrastructure.Data.Repositories;

namespace Infrastructure.Data.Repositories.CustomRepo
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public new RegistrationDbContext Context
        {
            get
            {
                return (RegistrationDbContext)base.Context;
            }
        }
        public UserRepository(RegistrationDbContext registrationDbContext) : base(registrationDbContext)
        {
        }

        public User GetUserByEmailAndPassword(string email, string password)
        {
            return objectSet.Include(r => r.Role).Where(u => u.Email == email && u.Password == password && u.IsActive ).SingleOrDefault();
        }

        public List<User> GetAllRequests()
        {
            return objectSet.Where(u=>u.RoleId==(int)RoleEnums.Student && u.IsActionTaken==false).ToList();
        }

        public User GetByEmail(string email)
        {
            return objectSet.Where(u => u.Email == email).FirstOrDefault();
        }

        public List<User> GetNotApprovedRejectedRequestsAfterThreeDays()
        {
            try
            {
                return objectSet.ToList();

            }
            catch (Exception e)
            {

                throw;
            }
        }
    }
}

using Core.Application.Interfaces.ICustomRepo;
using Core.Domain.Entities;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using RegistrationTask.Infrastructure.Data.Repositories;
using RegistrationTask.ViewModels;

namespace Infrastructure.Data.Repositories.CustomRepo
{
    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public new RegistrationDbContext Context
        {
            get
            {
                return (RegistrationDbContext)base.Context;
            }
        }
        public RoleRepository(RegistrationDbContext registrationDbContext) : base(registrationDbContext)
        {
        }

       

        public Task<List<NameIdVm>> GetNames()
        {
            return objectSet.Select(p => new NameIdVm { Id = p.Id, Name = p.Name}).ToListAsync();
        }
    }
}

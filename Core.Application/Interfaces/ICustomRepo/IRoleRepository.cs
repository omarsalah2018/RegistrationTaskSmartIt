using Core.Domain.Entities;
using RegistrationTask.Core.Application.Interfaces;
using RegistrationTask.ViewModels;

namespace Core.Application.Interfaces.ICustomRepo
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
        Task<List<NameIdVm>> GetNames();
    }
}

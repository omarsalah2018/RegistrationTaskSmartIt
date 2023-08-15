using RegistrationTask.ViewModels;

namespace Core.Application.Services.IServices
{
    public interface IRoleService
    {
        Task<List<NameIdVm>> GetNames();

    }
}

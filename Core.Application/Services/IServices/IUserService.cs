using Core.Application.Dtos;
using RegistrationTask.ViewModels;

namespace Core.Application.Services.IServices
{
    public interface IUserService
    {
        List<UserDto> GetAllRequests();
        Task<UserDto> GetById(int id);
        string Add(AddUserVm userVm);
        Task<string> ApproveRejectStudent(int id, bool stuts);
        void DeleteNotApprovedRejectedRequests();
    }
}

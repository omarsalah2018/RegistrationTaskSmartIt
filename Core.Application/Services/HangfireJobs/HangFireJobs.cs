using Core.Application.Interfaces.ICustomRepo;
using Core.Application.Services.Services;
using Core.Domain.Entities;
using Microsoft.Extensions.Logging;
using RegistrationTask.Core.Application.Interfaces;

namespace Core.Application.Services.HangfireJobs
{
    public class HangFireJobs : IHangFireJobs
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<RoleService> _logger;
        public HangFireJobs(IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<RoleService> logger)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        //Requests Not Being Approved Or Rejected For 3 Days Must Be Automatically Deleted
        public void DeleteNotApprovedRejectedRequests()
        {
          //  List<User> users = _userRepository.GetNotApprovedRejectedRequestsAfterThreeDays();
        }
    }
}

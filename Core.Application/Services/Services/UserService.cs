using Core.Application.Constants;
using Core.Application.Dtos;
using Core.Application.Enums;
using Core.Application.Interfaces.ICustomRepo;
using Core.Application.Services.IServices;
using Core.Domain.Entities;
using Mapster;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using RegistrationTask.Core.Application.Interfaces;
using RegistrationTask.Core.Application.SendNotificationRealTime;
using RegistrationTask.ViewModels;
using System.Runtime.CompilerServices;

namespace Core.Application.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<RoleService> _logger;
        private readonly IHubContext<NotifyHub, ITypedHubClient> _hubContext;

        private List<UserDto> userDtos = new List<UserDto>();
        private List<User> users = new List<User>();
        private User user = new User();
        private UserDto userDto = new UserDto();
        public UserService() { }
        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, ILogger<RoleService> logger, IHubContext<NotifyHub, ITypedHubClient> hubContext)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
            _hubContext = hubContext;
        }
        public string Add(AddUserVm userVm)
        {
            try
            {
                //User Can Not Register Again If He Has Pending Or Approved Request
                User userInDb = _userRepository.GetByEmail(userVm.Email);
                if (userInDb.IsActionTaken || userInDb.IsApproved) return "this user can register now";

                User userEntity = new User();
                userEntity = userVm.Adapt(userEntity);
                userEntity.CreatedAt = DateTime.Now;
                userEntity.UpdatedAt = DateTime.Now;
                userEntity.RoleId = (int)RoleEnums.Student;
                _userRepository.Add(userEntity);
                var added = _unitOfWork.Complete();
                if (added <= 0)
                    return EndUserMessages.SOMETHING_WRONG;


                #region Send Request Notification
                SignalRMessage message = new SignalRMessage();
                message.Information = "New Request Added";
                _hubContext.Clients.All.BroadCastMessage(message);
                #endregion

                return string.Empty;
            }
            catch (Exception e)
            {
                _logger.LogError(EndUserMessages.CATCH_EXCEPTION + e.StackTrace);
                throw;
            }
        }

        public async Task<string> ApproveRejectStudent(int id, bool status)
        {
            try
            {
                user = await _userRepository.GetById(id);
                if (user == null) return EndUserMessages.NOT_FOUND;

                user.IsApproved = status;
                user.IsActionTaken = true;
                user.UpdatedAt = DateTime.Now;

                _userRepository.Update(user);
                _unitOfWork.Complete();
                return string.Empty;

            }
            catch (Exception e)
            {
                _logger.LogError(EndUserMessages.CATCH_EXCEPTION + e.StackTrace);
                throw;
            }

        }

        public List<UserDto> GetAllRequests()
        {
            users = _userRepository.GetAllRequests();
            userDtos = users.Adapt(userDtos);
            return userDtos;
        }
        public async Task<UserDto> GetById(int id)
        {
            user = await _userRepository.GetById(id);
            userDto = user.Adapt(userDto);
            return userDto;
        }
        public void DeleteNotApprovedRejectedRequests()
        {
            try
            {
                List<User> users = _userRepository.GetNotApprovedRejectedRequestsAfterThreeDays();
                if (users != null)
                {
                    _userRepository.RemoveRange(users);
                    _unitOfWork.Complete();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(EndUserMessages.CATCH_EXCEPTION + e.StackTrace);
                throw;
            }
        }

    }



}

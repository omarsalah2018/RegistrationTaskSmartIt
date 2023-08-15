using Core.Application.Constants;
using Core.Application.Services.IServices;
using Core.Application.Services.Services;
using Hangfire;
using Hangfire.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RegistrationTask.ViewModels;

namespace RegistrationTask.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;
        private readonly IRecurringJobManager _recurringJobManager;
        public UsersController(IUserService userService, ILogger<UsersController> logger, IRecurringJobManager recurringJobManager)
        {
            _userService = userService;
            _logger = logger;
            _recurringJobManager = recurringJobManager;
        }
        [HttpGet("get-all-requests")]
        public  IActionResult GetAllRequests()
        {
            var users =  _userService.GetAllRequests();
            return Ok(users);

        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetById([FromRoute] int userId)
        {
          var user=  _userService.GetById(userId);
            return Ok(user);
        }
        [HttpPost("register-student")]
        public ActionResult Add([FromBody] AddUserVm addUser)
        {
            try
            {
              var res= _userService.Add(addUser);
                return Ok(new {Message= EndUserMessages.ADDED });
            }
            catch (Exception e)
            {
                _logger.LogError(EndUserMessages.CATCH_EXCEPTION + e.Message);
                return BadRequest(EndUserMessages.CATCH_EXCEPTION);
            }
        }
        [HttpPut("approve-reject/{id}/{status}")]
        public async Task<ActionResult> ActivateDeactivate([FromRoute]int id, [FromRoute] bool status)
        {
            try
            {
                string res = await _userService.ApproveRejectStudent(id,status);
                if (!string.IsNullOrWhiteSpace(res))
                    return BadRequest(res);

                return Ok(new { Message = EndUserMessages.DONE});
            }
            catch (Exception e)
            {
                _logger.LogError(EndUserMessages.CATCH_EXCEPTION + e.InnerException.Message);
                return BadRequest(EndUserMessages.CATCH_EXCEPTION);
            }
        
        }
        [HttpGet("run-job")]
        public ActionResult RunJob()
        {
            var client = new BackgroundJobClient();
            UserService userServiceObj = new UserService();
            _recurringJobManager.AddOrUpdate("DeleteNotApprovedRejected",  () =>  userServiceObj.DeleteNotApprovedRejectedRequests(), Cron.Minutely());

            return Ok();
        }

    }
}

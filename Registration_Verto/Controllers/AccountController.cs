using Core.Application.Dtos;
using Core.Application.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RegistrationTask.ViewModels;

namespace RegistrationTask.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountUserService _accountService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(IAccountUserService accountService, ILogger<AccountController> logger)
        {
            _accountService = accountService;
            _logger = logger;
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult Login([FromBody] LoginVM loginVm)
        {
            try
            {
                UserDto userDto = _accountService.Login(loginVm.Email, loginVm.Password);
                if (userDto == null) return BadRequest("User not found");

                //create token
                var token = _accountService.GenerateToken(userDto);

                return Ok(new { Token = token, UserObj = userDto });
            }
            catch (Exception ex)
            {
                _logger.LogError(0, ex.StackTrace);
                return BadRequest(ex.Message);
            }
        }
    }
}

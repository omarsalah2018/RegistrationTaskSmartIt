using Core.Application.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using RegistrationTask.ViewModels;

namespace RegistrationTask.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        private readonly ILogger<RolesController> _logger;

        public RolesController(IRoleService roleService, ILogger<RolesController> logger)
        {
            _roleService = roleService;
            _logger = logger;
        }
       
        [HttpGet("get-names")]
        public async Task<ActionResult<List<NameIdVm>>> GetNames()
        {
            var roles = await _roleService.GetNames();
            return Ok(roles);
        }
       
        
    }
}

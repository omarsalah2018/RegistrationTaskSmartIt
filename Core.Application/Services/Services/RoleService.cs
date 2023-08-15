using Core.Application.Constants;
using Core.Application.Dtos;
using Core.Application.Interfaces.ICustomRepo;
using Core.Application.Services.IServices;
using Core.Domain.Entities;
using Mapster;
using Microsoft.Extensions.Logging;
using RegistrationTask.Core.Application.Interfaces;
using RegistrationTask.ViewModels;

namespace Core.Application.Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        private List<RoleDto> roleDtos = new List<RoleDto>();
        private List<Role> roles = new List<Role>();
        private Role role = new Role();
        private RoleDto roleDto = new RoleDto();
        private string error = "";
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;

        }


        public async Task<List<NameIdVm>> GetNames()
        {
            List<NameIdVm> names = await _roleRepository.GetNames();
            return names;
        }
    }
}

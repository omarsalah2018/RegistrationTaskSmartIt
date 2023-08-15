using Core.Application.Interfaces.ICustomRepo;
using Core.Application.Services.IServices;
using Core.Application.Services.Services;
using Infrastructure.Data.Contexts;
using Infrastructure.Data.Repositories.CustomRepo;
using Microsoft.Extensions.DependencyInjection;
using RegistrationTask.Core.Application.Interfaces;
using RegistrationTask.Core.Application.Services.Service;
using RegistrationTask.Infrastructure.Data.Repositories;

namespace Infrastructure.Shared
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {

            #region Repositories
            services.AddDbContext<RegistrationDbContext>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
          
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            #region Services
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountUserService, AccountUserService>();
            #endregion

            #region MediatR
            //services.AddMediatR(typeof(GetAllUserHandler));
            #endregion

            //services.AddValidatorsFromAssemblyContaining<RoleValidator>();
            //services.AddScoped<IValidator<Role>, RoleValidator>();
        }
    }
}

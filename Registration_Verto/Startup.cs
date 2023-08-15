using Core.Application.Services.HangfireJobs;
using Core.Application.Services.IServices;
using Core.Application.Services.Services;
using Hangfire;
using Infrastructure.Data.Contexts;
using Infrastructure.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.IdentityModel.Tokens;
using RegistrationTask.Core.Application.SendNotificationRealTime;
using System.Text;

namespace RegistrationTask
{
    public class Startup
    {
        private IConfiguration _configuration { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IHangFireJobs, HangFireJobs>();

            services.AddDbContext<RegistrationDbContext>();
            services.AddControllersWithViews();
            //.AddFluentValidation(s =>
            //{
            //    s.RegisterValidatorsFromAssemblyContaining<RoleValidator>();

            //    s.DisableDataAnnotationsValidation =false;

            //});
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            // services.AddEndpointsApiExplorer();
            services.AddHangfire(x => x.UseSqlServerStorage(_configuration.GetConnectionString("DefaultConnection")));
            services.AddHangfireServer();

            DependencyContainer.RegisterServices(services);

            services.AddSwaggerGen();
            services.AddSignalR();



            //services cors
            services.AddCors(p => p.AddPolicy("corsapp", builder =>
            {
                builder.AllowAnyMethod().AllowAnyHeader()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials(); // allow credentials
            }));

            #region Jwt
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                var key = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
                o.SaveToken = true;
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["JWT:Issuer"],
                    ValidAudience = _configuration["JWT:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
            #endregion
        }
        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            //if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCors("corsapp");
           // app.UseAuthorization();
            app.UseStaticFiles();
            app.MapControllers();
            app.MapHub<NotifyHub>("notify", options =>
            {
                options.Transports = HttpTransportType.WebSockets;
            });
            app.UseHangfireDashboard();

            app.Run();
        }

    }
}

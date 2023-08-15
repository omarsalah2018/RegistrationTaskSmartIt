using Core.Domain.Entities;
using Infrastructure.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Infrastructure.Shared
{
    public class DataSeeder
    {
        public static void SeedRoles(RegistrationDbContext context)
        {
            if (!context.Roles.Any())
            {
                var roles = new List<Role>{
                    new Role{Id=2,Name = "Student",CreatedAt = DateTime.Now,Description = "For Student",IsActive = true,CreatedBy = 0},
                    new Role{Id=1,Name = "Admin",CreatedAt = DateTime.Now,Description = "For Admin",IsActive = true,CreatedBy = 0}

            };
                context.AddRange(roles);
                context.SaveChanges();
            }
        }
    }
}

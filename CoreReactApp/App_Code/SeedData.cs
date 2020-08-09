using CoreReactApp.Domain.Models;
using CoreReactApp.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CoreReactApp
{
    public static class SeedData
    {

        public static void EnsurePopulated(IApplicationBuilder applicationBuilder)
        {
            ApplicationDbContext context = applicationBuilder.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();

            if (!context.Incidents.Any())
            {
                var category1 = new Category { Id = Guid.NewGuid(), Name = "Концерт" };
                var category2 = new Category { Id = Guid.NewGuid(), Name = "Open Air" };
                context.Categories.AddRange(category1, category2);
                context.Incidents.AddRange(
                    new Incident
                    {
                        Id = Guid.NewGuid(),
                        Title = "Концерт оч популярной группы",
                        Description = "Вообще будет круто инфа 100",
                        IncidentDate = new DateTime(2020, 09, 01, 20, 00, 00).ToUniversalTime(),
                        IncidentCategories = new List<IncidentCategory>()
                        {
                            new IncidentCategory { CategoryId = category1.Id},
                            new IncidentCategory { CategoryId = category2.Id }
                        }
                    },
                    new Incident
                    {
                        Id = Guid.NewGuid(),
                        Title = "Концерт неоч популярной группы",
                        Description = "Непонятно что за ребята",
                        IncidentDate = new DateTime(2020, 07, 01, 20, 00, 00).ToUniversalTime(),
                        IncidentCategories = new List<IncidentCategory>()
                        {
                            new IncidentCategory { CategoryId = category1.Id },
                            new IncidentCategory { CategoryId = category2.Id }
                        }
                    });
                context.SaveChanges();
            }

            if (!context.Users.Any())
            {
                context.Users.AddRange(new User
                {
                    Id = Guid.NewGuid(),
                    Login = "admin",
                    Role = RoleEnum.Administrator,
                    PasswordHash = "12345"
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Login = "moder",
                    Role = RoleEnum.Moderator,
                    PasswordHash = "12345"
                });
                context.SaveChanges();
            }
        }

    }
}

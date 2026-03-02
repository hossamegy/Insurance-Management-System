using InsuranceAdministration.Core.Interfaces.Repository;
using InsuranceAdministration.Infrastructure.Persistance;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InsuranceAdministration.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static void AddInfrastructureRegistration(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IMissionRepository, MissionRepository>();
            builder.Services.AddScoped<ISoldierRepository, SoldierRepository>();

            // Settings
            builder.Services.AddScoped<ISettingsRepository, SettingsRepository>();


        }
    }
}

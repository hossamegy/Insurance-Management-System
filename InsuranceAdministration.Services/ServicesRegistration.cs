using InsuranceAdministration.Core.Interfaces.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace InsuranceAdministration.Services
{
    public static class ServicesRegistration
    {
        public static void AddServicesRegistration(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IMissionServices, MissionService>();
            builder.Services.AddScoped<ISoldierServices, SoldierServices>();

            // Settings
            builder.Services.AddScoped<ISettingsServices, SettingsServices>();

        }
    }
}

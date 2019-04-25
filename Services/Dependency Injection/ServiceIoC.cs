using Microsoft.Extensions.DependencyInjection;
using Services.Interfaces;

namespace Services.Dependency_Injection
{
    public static class ServiceIoC
    {
        public static void ApplicationServiceIoC(this IServiceCollection services)
        {
            services.AddScoped(typeof(IServiceBase<>), typeof(ServiceBase<>));

            services.AddScoped<ISchoolService, SchoolService>();
            services.AddScoped<IClassService, ClassService>();
        }
    }
}

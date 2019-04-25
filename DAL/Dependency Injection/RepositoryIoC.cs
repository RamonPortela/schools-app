using DAL.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Dependency_Injection
{
    public static class RepositoryIoC
    {
        public static void ApplicationRepositoryIoC(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));

            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
        }
    }
}

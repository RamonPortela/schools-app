using DAL.DBConfig;
using DAL.DBConfig.EntityFramework;
using DAL.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Services.Dependency_Injection
{
    public static class RepositoryIoC
    {
        public static void ApplicationRepositoryIoC(this IServiceCollection services, IConfiguration configuration = null)
        {
            IConfiguration dbConnectionSettings = ResolveConfiguration.GetConnectionSettings(configuration);
            string conn = dbConnectionSettings.GetConnectionString("localDbConnection");
            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(conn));

            services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
            services.AddScoped<ISchoolRepository, SchoolRepository>();
            services.AddScoped<IClassRepository, ClassRepository>();
        }
    }

    internal class ResolveConfiguration
    {        public static IConfiguration GetConnectionSettings(IConfiguration configuration)
        {
            return configuration ?? DatabaseConnection.ConnectionConfiguration;
        }
    }
}

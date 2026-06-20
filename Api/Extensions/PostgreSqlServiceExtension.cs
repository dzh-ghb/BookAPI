using Api.Data;
using Microsoft.EntityFrameworkCore;

namespace Api.Extensions
{
    public static class PostgreSqlServiceExtension
    {
        public static void AddPostgreSqlDbContext(
            this IServiceCollection services,
            IConfiguration configuration
        )
        {
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection"));
            });
        }
    }
}
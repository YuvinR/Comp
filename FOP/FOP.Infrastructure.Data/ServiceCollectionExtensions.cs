using FOP.Core.Entities;
using FOP.Core.Entities.Interfaces;
using FOP.Core.Entities.Interfaces.Repository;
using FOP.Core.Services;
using FOP.Infrastructure.Data.Data;
using FOP.Infrastructure.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GIC.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var cs = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IFileUploadRepository, FileUploadRepository>();
            services.AddScoped<IFileUploadService, FileUploadService>();

            services.AddScoped<IMonthlyUploadRepository, MonthlyUploadRepository>();
            services.AddScoped<IMonthlyUploadService, MonthlyUploadService>();

            services.AddScoped<IAllMasterRepository, AllMasterRepository>();
            services.AddScoped<IAllMasterService, AllMasterService>();
            return services;
        }
        
        
    }
}

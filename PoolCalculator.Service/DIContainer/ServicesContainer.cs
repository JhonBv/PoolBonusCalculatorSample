using Microsoft.Extensions.DependencyInjection;
using PoolCalculator.Domain;
using PoolCalculator.Service.Logging;
using PoolCalculator.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolCalculator.Service.DIContainer
{
    public static class ServicesContainer
    {
        public static void AddServices(IServiceCollection services)
        {
            services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddTransient<IBonusPoolService, BonusPoolService>();
            services.AddTransient<ILoggerManager, LoggerManager>();
        }

        public static void InjectServices(this IServiceCollection services)
        {
            AddServices(services);
        }
    }
}

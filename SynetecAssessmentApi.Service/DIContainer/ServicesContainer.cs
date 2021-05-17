using Microsoft.Extensions.DependencyInjection;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Service.Logging;
using SynetecAssessmentApi.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Service.DIContainer
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

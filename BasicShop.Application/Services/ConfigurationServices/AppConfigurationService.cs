using BasicShop.Core.ServiceInterfaces.ConfigurationsInterfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicShop.Application.Services.ConfigurationServices
{
    public class AppConfigurationService : IAppConfigurationService
    {
        private IConfiguration _configuration;

        public AppConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetConnectionString()
        {
            return _configuration.GetConnectionString("Default");
        }
    }
}

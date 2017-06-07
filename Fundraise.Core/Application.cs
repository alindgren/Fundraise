using Fundraise.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fundraise
{
    public class Application
    {
        public IServiceProvider Services { get; set; }
        public ILogger Logger { get; set; }
        public Application(IServiceCollection serviceCollection)
        {
            ConfigureServices(serviceCollection);
            Services = serviceCollection.BuildServiceProvider();
            Logger = Services.GetRequiredService<ILoggerFactory>()
                    .CreateLogger<Application>();
            Logger.LogInformation("Application created successfully.");
        }

        public void MakeDonation()
        {
            Logger.LogInformation("Begin making a donation");
            IDonationService donationService =
              Services.GetRequiredService<IDonationService>();
            donationService.Donate(10.5);
        }

        private void ConfigureServices(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IDonationService, DonationService>();
        }
    }
}

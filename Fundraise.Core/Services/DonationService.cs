using System;
using Microsoft.Extensions.Logging;

namespace Fundraise.Services
{
    public class DonationService : IDonationService
    {
        public ILogger Logger { get; }
        public DonationService(ILoggerFactory loggerFactory)
        {
            Logger = loggerFactory?.CreateLogger<DonationService>();
            if (Logger == null)
            {
                throw new ArgumentNullException(nameof(loggerFactory));
            }
            Logger.LogInformation("DonationService created");
        }

        public void Donate(double amount)
        {
            Logger.LogInformation("Donate amount: " + amount);
        }
    }
}

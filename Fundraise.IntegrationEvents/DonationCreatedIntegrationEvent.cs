using System;

namespace Fundraise.IntegrationEvents
{
    public class DonationCreatedIntegrationEvent : IntegrationEvent
    {
        public string UserId { get; private set; }
        public Guid FundraiserId { get; private set; }

        /// <summary>
        /// Donation amount in dollars
        /// </summary>
        public double DonationAmount { get; private set; }

        public string DonorDisplayName { get; private set; }

        public DonationCreatedIntegrationEvent(string userId, Guid fundraiserId, double donationAmount, string donorDisplayName)
        {
            UserId = userId;
            FundraiserId = fundraiserId;
            DonationAmount = donationAmount;
            DonorDisplayName = donorDisplayName;
        }
    }
}

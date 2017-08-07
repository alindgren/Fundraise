using Fundraise.Core.Entities;
using System;
using System.Collections.Generic;

namespace Fundraise.Core.Services
{
    public interface IDonationRepository
    {
        Donation Create(Campaign campaign, Fundraiser fundraiser, DonationStatus status, 
            double amount, string currencyCode, double amountInDefaultCurrency,
            string donorUserId = null, string donorDisplayName = null, string referenceNumber = null);
        IEnumerable<Donation> GetByCampaign(Guid campaignId);
        IEnumerable<Donation> GetByFundraiser(Guid fundraiserId);
        IEnumerable<Donation> GetByDonor(string userId);
    }
}

using Fundraise.Core.Entities;
using System;
using System.Collections.Generic;

namespace Fundraise.Core.Services
{
    public interface IDonationRepository
    {
        Donation Create(Campaign campaign, DonationStatus status, string donorDisplayName = null, 
            string referenceNumber = null);
        IEnumerable<Donation> GetAll(Guid campaignId);
    }
}

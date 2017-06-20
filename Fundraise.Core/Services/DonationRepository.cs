using System;
using System.Collections.Generic;
using System.Linq;
using Fundraise.Core.Entities;

namespace Fundraise.Core.Services
{
    public class DonationRepository : IDonationRepository
    {
        private FundraiseContext _context;

        public DonationRepository(FundraiseContext context)
        {
            _context = context;
        }

        public Donation Create(Campaign campaign, DonationStatus status, string donorDisplayName = null, string referenceNumber = null)
        {
            var donation = new Donation { Campaign = campaign, Status = status, DonorDisplayName = donorDisplayName, ReferenceNumber = referenceNumber };
            _context.Donations.Add(donation);
            _context.SaveChanges();
            return donation;
        }

        public IEnumerable<Donation> GetAll(Guid campaignId)
        {
            return _context.Donations.Where(x => x.CampaignId == campaignId);
        }
    }
}

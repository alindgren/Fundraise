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

        public Donation Create(Campaign campaign, Fundraiser fundraiser, DonationStatus status, double amount, string currencyCode,
            double amountInDefaultCurrency, string donorDisplayName = null, string referenceNumber = null)
        {
            if (fundraiser != null && fundraiser.CampaignId != campaign.Id) // validate campaign
            {
                throw new InvalidOperationException("fundraiser campaign id does not match");
            }

            var donation = new Donation
            {
                Campaign = campaign,
                Fundraiser = fundraiser,
                Status = status,
                Amount = amount,
                CurrencyCode = currencyCode,
                AmountInDefaultCurrency = amountInDefaultCurrency,
                DonorDisplayName = donorDisplayName,
                ReferenceNumber = referenceNumber
            };

            if (currencyCode == campaign.DefaultCurrencyCode && amount != amountInDefaultCurrency)
            {
                throw new InvalidOperationException("invalid amount (does not match amount in default currency)");
            }

            _context.Donations.Add(donation);
            _context.SaveChanges();
            return donation;
        }

        public IEnumerable<Donation> GetByCampaign(Guid campaignId)
        {
            return _context.Donations.Where(x => x.CampaignId == campaignId);
        }

        public IEnumerable<Donation> GetByFundraiser(Guid fundraiserId)
        {
            return _context.Donations.Where(x => x.FundraiserId == fundraiserId);
        }
    }
}

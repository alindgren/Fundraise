using System;
using System.Collections.Generic;
using System.Linq;
using Fundraise.Core.Entities;
using Newtonsoft.Json.Linq;

namespace Fundraise.Core.Services
{
    public class CampaignRepository : ICampaignRepository
    {
        private FundraiseContext _context;

        public CampaignRepository(FundraiseContext context)
        {
            _context = context;
        }

        public Campaign Create(string name, string defaultCurrencyCode, JObject extendedData, DateTime? endDate)
        {
            var campaign = new Campaign { Name = name, DefaultCurrencyCode = defaultCurrencyCode, DateCreated = DateTime.Now, DateLastUpdated = DateTime.Now };
            if (extendedData != null)
            {
                campaign.ExtendedData = extendedData;
            }
            if (endDate.HasValue)
                campaign.EndDate = endDate.Value;
            _context.Campaigns.Add(campaign);
            _context.SaveChanges();
            return campaign;
        }

        public IEnumerable<Campaign> FindByName(string name)
        {
            return _context.Campaigns.Where(x => x.Name == name).OrderBy(c => c.Name).ToList();
        }

        public bool Exists(string name)
        {
            var campaigns = _context.Campaigns.Where(x => x.Name == name);
            if (campaigns.Count() == 0)
                return false;
            var campaign = campaigns.First();
            return campaign != null;
        }

        public IEnumerable<Campaign> GetAll()
        {
            return _context.Campaigns.OrderBy(c => c.Name).ToList();
        }

        public Campaign FindById(Guid id)
        {
            return _context.Campaigns.Find(id);
        }

        public Campaign Update(Campaign campaign)
        {
            campaign.DateLastUpdated = DateTime.Now;
            var updateCampaign = _context.Update(campaign);
            _context.SaveChanges();
            return updateCampaign.Entity;
        }

        public void Close(Guid id)
        {
            var campaign = this.FindById(id);

            if (campaign == null)
                throw new Exception("Campaign not found");

            campaign.IsActive = false;
            campaign.DateLastUpdated = DateTime.Now;
            _context.Update(campaign);
            _context.SaveChanges();
        }
    }
}

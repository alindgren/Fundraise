using System;
using System.Collections.Generic;
using System.Linq;
using Fundraise.Core.Entities;
using Newtonsoft.Json.Linq;

namespace Fundraise.Core.Services
{
    public class FundraiserRepository : IFundraiserRepository
    {
        private FundraiseContext _context;

        public FundraiserRepository(FundraiseContext context)
        {
            _context = context;
        }

        public Fundraiser Create(string name, Guid campaignId, FundraiserType type, JObject extendedData = null)
        {
            var fundraiser = new Fundraiser { Name = name, CampaignId = campaignId, FundraiserType = type, DateCreated = DateTime.Now, DateLastUpdated = DateTime.Now };
            if (extendedData != null)
            {
                fundraiser.ExtendedData = extendedData;
            }
            _context.Fundraisers.Add(fundraiser);
            _context.SaveChanges();
            return fundraiser;
        }
        public IEnumerable<Fundraiser> FindByName(string name)
        {
            return _context.Fundraisers.Where(x => x.Name == name).OrderBy(c => c.Name).ToList();
        }

        public bool Exists(string name)
        {
            return _context.Fundraisers.Where(x => x.Name == name).First() != null;
        }

        public IEnumerable<Fundraiser> GetAll()
        {
            return _context.Fundraisers.OrderBy(c => c.Name).ToList();
        }

        public Fundraiser FindById(Guid id)
        {
            return _context.Fundraisers.Find(id);
        }

        public IEnumerable<Fundraiser> FindByCampaign(Guid campaignId)
        {
            return _context.Fundraisers.Where(x => x.CampaignId == campaignId).OrderBy(c => c.Name).ToList();
        }

        public Fundraiser Update(Fundraiser fundraiser)
        {
            fundraiser.DateLastUpdated = DateTime.Now;
            var updatedFundraiser = _context.Update(fundraiser);
            _context.SaveChanges();
            return updatedFundraiser.Entity;
        }

        public void Close(Guid id)
        {
            var fundraiser = this.FindById(id);

            if (fundraiser == null)
                throw new Exception("Fundraiser not found");

            fundraiser.DateLastUpdated = DateTime.Now;
            fundraiser.IsActive = false;
            _context.Update(fundraiser);
            _context.SaveChanges();
        }
    }
}

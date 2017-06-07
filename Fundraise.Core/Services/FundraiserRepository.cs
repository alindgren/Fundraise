using System;
using System.Collections.Generic;
using System.Linq;
using Fundraise.Core.Entities;

namespace Fundraise.Core.Services
{
    public class FundraiserRepository : IFundraiserRepository
    {
        private FundraiseContext _context;

        public FundraiserRepository(FundraiseContext context)
        {
            _context = context;
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

        public void Close(Guid id)
        {
            var fundraiser = this.FindById(id);

            if (fundraiser == null)
                throw new Exception("Fundraiser not found");

            fundraiser.IsActive = false;
            _context.Update(fundraiser);
            _context.SaveChanges();
        }
    }
}

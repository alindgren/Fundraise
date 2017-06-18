using Fundraise.Core.Entities;
using System;
using System.Collections.Generic;

namespace Fundraise.Core.Services
{
    public interface ICampaignRepository
    {
        Campaign Create(string name, string defaultCurrencyCode, DateTime? endDate = null);
        IEnumerable<Campaign> GetAll();
        Campaign FindById(Guid id);
        IEnumerable<Campaign> FindByName(string name);
        bool Exists(string name);
        void Close(Guid id);
    }
}

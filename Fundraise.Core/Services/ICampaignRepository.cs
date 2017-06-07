using Fundraise.Core.Entities;
using System;
using System.Collections.Generic;

namespace Fundraise.Core.Services
{
    public interface ICampaignRepository
    {
        IEnumerable<Campaign> GetAll();
        Campaign FindById(Guid id);
        IEnumerable<Campaign> FindByName(string name);
        bool Exists(string name);
        void Close(Guid id);
    }
}

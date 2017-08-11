using Fundraise.Core.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Fundraise.Core.Services
{
    public interface IFundraiserRepository
    {
        Fundraiser Create(string name, Guid campaignId, FundraiserType type, string creatorUserId, JObject extendedData = null);
        IEnumerable<Fundraiser> GetAll();
        Fundraiser FindById(Guid id);
        IEnumerable<Fundraiser> FindByName(string name);
        IEnumerable<Fundraiser> FindByCampaign(Guid campaignId);
        IEnumerable<Fundraiser> FindByCreator(string userId);
        bool Exists(string name);
        Fundraiser Update(Fundraiser fundraiser);
        void Close(Guid id);
    }
}

using Fundraise.Core.Entities;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Fundraise.Core.Services
{
    public interface IFundraiserRepository
    {
        Fundraiser Create(string name, Guid campaignId, FundraiserType type, JObject extendedData = null);
        IEnumerable<Fundraiser> GetAll();
        Fundraiser FindById(Guid id);
        IEnumerable<Fundraiser> FindByName(string name);
        bool Exists(string name);
        Fundraiser Update(Fundraiser fundraiser);
        void Close(Guid id);
    }
}

using Fundraise.Core.Entities;
using System;
using System.Collections.Generic;

namespace Fundraise.Core.Services
{
    public interface IFundraiserRepository
    {
        IEnumerable<Fundraiser> GetAll();
        Fundraiser FindById(Guid id);
        IEnumerable<Fundraiser> FindByName(string name);
        bool Exists(string name);
        void Close(Guid id);
    }
}

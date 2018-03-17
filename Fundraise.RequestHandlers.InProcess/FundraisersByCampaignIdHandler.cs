using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.Requests.Fundraiser;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class FundraisersByCampaignIdHandler : RequestHandler<GetByCampaignId, List<Fundraiser>>
    {
        private IFundraiserRepository _fundraiserRepository;

        public FundraisersByCampaignIdHandler(IFundraiserRepository fundraiserRepository)
        {
            _fundraiserRepository = fundraiserRepository;
        }

        protected override List<Fundraiser> HandleCore(GetByCampaignId request)
        {
            return _fundraiserRepository.FindByCampaign(request.CampaignId).ToList();
        }
    }
}
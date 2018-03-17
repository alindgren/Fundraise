using Fundraise.Core.Services;
using Fundraise.Requests.Fundraiser;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Fundraise.RequestHandlers.InProcess.Fundraiser
{
    public class GetByCampaignIdHandler : RequestHandler<GetByCampaignId, List<Core.Entities.Fundraiser>>
    {
        private IFundraiserRepository _fundraiserRepository;

        public GetByCampaignIdHandler(IFundraiserRepository fundraiserRepository)
        {
            _fundraiserRepository = fundraiserRepository;
        }

        protected override List<Core.Entities.Fundraiser> HandleCore(GetByCampaignId request)
        {
            return _fundraiserRepository.FindByCampaign(request.CampaignId).ToList();
        }
    }
}
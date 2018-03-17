using Fundraise.Core.Services;
using Fundraise.Requests.Campaign;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Fundraise.RequestHandlers.InProcess.Campaign
{
    public class GetAllHandler : RequestHandler<GetAll, List<Core.Entities.Campaign>>
    {
        private ICampaignRepository _campaignRepository;

        public GetAllHandler(CampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        protected override List<Core.Entities.Campaign> HandleCore(GetAll request)
        {
            return _campaignRepository.GetAll().ToList();
        }
    }
}
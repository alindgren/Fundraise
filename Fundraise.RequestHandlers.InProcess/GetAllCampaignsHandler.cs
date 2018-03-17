using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.Requests.Campaign;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class GetAllCampaignsHandler : RequestHandler<GetAll, List<Campaign>>
    {
        private ICampaignRepository _campaignRepository;

        public GetAllCampaignsHandler(CampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        protected override List<Campaign> HandleCore(GetAll request)
        {
            return _campaignRepository.GetAll().ToList();
        }
    }
}
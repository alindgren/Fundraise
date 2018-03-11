using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.MvcExample.Requests;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class GetAllCampaignsHandler : RequestHandler<GetAllCampaigns, List<Campaign>>
    {
        private ICampaignRepository _campaignRepository;

        public GetAllCampaignsHandler(CampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        protected override List<Campaign> HandleCore(GetAllCampaigns request)
        {
            return _campaignRepository.GetAll().ToList();
        }
    }
}
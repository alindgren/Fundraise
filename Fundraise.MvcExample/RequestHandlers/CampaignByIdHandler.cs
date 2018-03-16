using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.Requests.Campaign;
using MediatR;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class CampaignByIdHandler : RequestHandler<GetById, Campaign>
    {
        private ICampaignRepository _campaignRepository;

        public CampaignByIdHandler(CampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        protected override Campaign HandleCore(GetById request)
        {
            return _campaignRepository.FindById(request.CampaignId);
        }
    }
}
using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.MvcExample.Requests;
using MediatR;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class CampaignByIdHandler : RequestHandler<CampaignById, Campaign>
    {
        private ICampaignRepository _campaignRepository;

        public CampaignByIdHandler(CampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        protected override Campaign HandleCore(CampaignById request)
        {
            return _campaignRepository.FindById(request.CampaignId);
        }
    }
}
using Fundraise.Core.Services;
using Fundraise.Requests.Campaign;
using MediatR;

namespace Fundraise.RequestHandlers.InProcess.Campaign
{
    public class GetByIdHandler : RequestHandler<GetById, Core.Entities.Campaign>
    {
        private ICampaignRepository _campaignRepository;

        public GetByIdHandler(CampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        protected override Core.Entities.Campaign HandleCore(GetById request)
        {
            return _campaignRepository.FindById(request.CampaignId);
        }
    }
}
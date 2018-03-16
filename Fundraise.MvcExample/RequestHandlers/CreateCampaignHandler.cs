using Fundraise.Core.Services;
using Fundraise.Requests.Campaign;
using MediatR;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class CreateCampaignHandler : RequestHandler<Create, bool>
    {
        private ICampaignRepository _campaignRepository;

        public CreateCampaignHandler(CampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        protected override bool HandleCore(Create request)
        {
            try
            {
                var campaign = _campaignRepository.Create(request.Name, request.DefaultCurrencyCode);
                campaign.Description = request.Description;
                _campaignRepository.Update(campaign);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
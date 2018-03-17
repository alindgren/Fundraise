using Fundraise.Core.Services;
using Fundraise.Requests.Campaign;
using MediatR;

namespace Fundraise.RequestHandlers.InProcess.Campaign
{
    public class CreateHandler : RequestHandler<Create, bool>
    {
        private ICampaignRepository _campaignRepository;

        public CreateHandler(CampaignRepository campaignRepository)
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
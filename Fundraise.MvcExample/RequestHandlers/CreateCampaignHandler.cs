using Fundraise.Core.Services;
using Fundraise.MvcExample.Requests;
using MediatR;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class CreateCampaignHandler : RequestHandler<CreateCampaign, bool>
    {
        private ICampaignRepository _campaignRepository;

        public CreateCampaignHandler(CampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        protected override bool HandleCore(CreateCampaign request)
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
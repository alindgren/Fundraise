using Fundraise.Core.Services;
using Fundraise.MvcExample.Requests;
using MediatR;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class UpdateCampaignHandler : RequestHandler<UpdateCampaign, bool>
    {
        private ICampaignRepository _campaignRepository;

        public UpdateCampaignHandler(CampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        protected override bool HandleCore(UpdateCampaign request)
        {
            try
            {
                var campaign = _campaignRepository.FindById(request.Id);
                campaign.Name = request.Name;
                campaign.Description = request.Description;
                campaign.DefaultCurrencyCode = request.DefaultCurrencyCode;
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
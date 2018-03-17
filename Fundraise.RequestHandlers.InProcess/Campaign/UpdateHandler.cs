using Fundraise.Core.Services;
using Fundraise.Requests.Campaign;
using MediatR;

namespace Fundraise.RequestHandlers.InProcess.Campaign
{
    public class UpdateHandler : RequestHandler<Update, bool>
    {
        private ICampaignRepository _campaignRepository;

        public UpdateHandler(CampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        protected override bool HandleCore(Update request)
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
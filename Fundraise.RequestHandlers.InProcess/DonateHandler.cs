using System;
using Fundraise.Core.Entities;
using Fundraise.Core.Services;
using Fundraise.IntegrationEvents;
using Fundraise.Requests;
using MediatR;
using Stripe;

namespace Fundraise.MvcExample.RequestHandlers
{
    public class DonateHandler : RequestHandler<Donate, bool>
    {
        private ICampaignRepository _campaignRepository;
        private IFundraiserRepository _fundraiserRepository;
        private IDonationRepository _donationRepository;
        private IEventBus _eventBus;

        public DonateHandler(ICampaignRepository campaignRepository, IFundraiserRepository fundraiserRepository, IDonationRepository donationRepository, IEventBus eventBus)
        {
            _campaignRepository = campaignRepository;
            _fundraiserRepository = fundraiserRepository;
            _donationRepository = donationRepository;
            _eventBus = eventBus;
        }

        protected override bool HandleCore(Donate request)
        {
            var fundraiser = _fundraiserRepository.FindById(request.FundraiserId);
            var campaign = _campaignRepository.FindById(fundraiser.CampaignId);

            if (request.DonationAmount > 0)
            {
                try
                {
                    var chargeService = new StripeChargeService();
                    var charge = chargeService.Create(new StripeChargeCreateOptions()
                    {
                        Amount = request.DonationAmount * 100,
                        Currency = "usd",
                        Description = fundraiser.Name,
                        SourceTokenOrExistingSourceId = request.StripeToken
                    });
                    var donation = _donationRepository.Create(campaign, fundraiser, DonationStatus.Completed, request.DonationAmount, "usd", request.DonationAmount, request.UserId, request.DonorDisplayName, charge.Id);

                    var integrationEvent = new DonationCreatedIntegrationEvent(donation.DonorUserId,
                        donation.FundraiserId, donation.Amount, donation.DonorDisplayName);

                    _eventBus.Publish(integrationEvent); // not tested; maybe IEventBus should be named something else since EasyNetQ already has an IEventBus; perhaps Publish(e) should be Publish<T>(e)?
                }
                catch (InvalidOperationException)
                {
                    return false; // maybe should return json with validation info?
                }
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}